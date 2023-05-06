
#include "cuda_runtime.h"
#include "device_launch_parameters.h"
#include <iostream>
#include <stdio.h>

using namespace std;

extern "C" { 
	__declspec(dllexport) void Detect() {
	int nDevices;
	cudaGetDeviceCount(&nDevices);
	printf("Number of CUDA Devices: %d\n", nDevices);
	for (int i = 0; i < nDevices; i++) {
		cudaDeviceProp prop;
		cudaGetDeviceProperties(&prop, i);
		printf("Device Number: %d\n", i);
		printf("  Device name: %s\n", prop.name);
		printf("  Memory Clock Rate (KHz): %d\n",
			prop.memoryClockRate);
		printf("  Memory Bus Width (bits): %d\n",
			prop.memoryBusWidth);
		printf("  Peak Memory Bandwidth (GB/s): %f\n\n",
			2.0*prop.memoryClockRate*(prop.memoryBusWidth / 8) / 1.0e6);
		printf("Number of multiprocessors:     %d\n", prop.multiProcessorCount);
	}
}
}

struct Rgb {
	unsigned char r, g, b;
};

__global__ void ProcessPixel_Negatyw(unsigned char* d_bitmapaWe, unsigned char* d_bitmapaWy, unsigned long int* d_size)
{
	unsigned long int id = threadIdx.x + blockIdx.x * 256;

	if (id > *d_size)
		return;

		d_bitmapaWy[id] = 255 - d_bitmapaWe[id];
}
__global__ void ProcessPixel_Jasnosc(unsigned char* d_bitmapaWe, unsigned char* d_bitmapaWy, unsigned long int* d_size)
{
	unsigned long int id = threadIdx.x + blockIdx.x * 256;

	if (id > *d_size)
		return;
	if (id % 3 != 1)
		return;
	unsigned char suma = (0.299*d_bitmapaWe[id-1] + 0.587*d_bitmapaWe[id] + 0.114*d_bitmapaWe[id+1]);
	d_bitmapaWy[id] = suma;
	d_bitmapaWy[id+1] = suma;
	d_bitmapaWy[id-1] = suma;
}
__global__ void ProcessPixel_Rozjasnianie(unsigned char* d_bitmapaWe, unsigned char* d_bitmapaWy, unsigned long int* d_size,int* d_wartosc)
{
	unsigned long int id = threadIdx.x + blockIdx.x * 256;

	if (id > *d_size)
		return;
	int suma = ((int)d_bitmapaWe[id] + *d_wartosc);
	if (suma > 255)
		suma = 255;
	if (suma < 0)
		suma = 0;
	d_bitmapaWy[id] = suma;
}

extern "C" {
	__declspec(dllexport) unsigned char* C_Negatyw(unsigned char* h_bitmapa, unsigned long int h_size)
	{
		
			unsigned char *d_bitmapaWe;
			unsigned char *d_bitmapaWy;

			unsigned long int *d_size = NULL;
			cudaMalloc((void**)&d_size, sizeof(unsigned long int));
			cudaMalloc((void**)&d_bitmapaWe, sizeof(unsigned char)* h_size);
			cudaMalloc((void**)&d_bitmapaWy, sizeof(unsigned char)* h_size);
			cudaMemcpy(d_bitmapaWe, h_bitmapa, h_size * sizeof(unsigned char), cudaMemcpyHostToDevice);
			cudaMemcpy(d_size, &h_size, sizeof(unsigned long int), cudaMemcpyHostToDevice);
			int blocks = (h_size / 256)+1;
			ProcessPixel_Negatyw << <blocks, 256 >> > (d_bitmapaWe, d_bitmapaWy, d_size);

			cudaThreadSynchronize();
			cudaMemcpy(h_bitmapa, d_bitmapaWy, h_size * sizeof(unsigned char), cudaMemcpyDeviceToHost);

			cudaFree(d_bitmapaWe);
			cudaFree(d_bitmapaWy);
			cudaFree(d_size);
			return h_bitmapa;
			
	}
}
extern "C" {
	__declspec(dllexport) unsigned char* C_Jasnosc(unsigned char* h_bitmapa, unsigned long int h_size)
	{

		unsigned char *d_bitmapaWe;
		unsigned char *d_bitmapaWy;

		unsigned long int *d_size = NULL;
		cudaMalloc((void**)&d_size, sizeof(unsigned long int));
		cudaMalloc((void**)&d_bitmapaWe, sizeof(unsigned char)* h_size);
		cudaMalloc((void**)&d_bitmapaWy, sizeof(unsigned char)* h_size);
		cudaMemcpy(d_bitmapaWe, h_bitmapa, h_size * sizeof(unsigned char), cudaMemcpyHostToDevice);
		cudaMemcpy(d_size, &h_size, sizeof(unsigned long int), cudaMemcpyHostToDevice);
		int blocks = (h_size / 256) + 1;
		ProcessPixel_Jasnosc << <blocks, 256 >> > (d_bitmapaWe, d_bitmapaWy, d_size);

		cudaThreadSynchronize();
		cudaMemcpy(h_bitmapa, d_bitmapaWy, h_size * sizeof(unsigned char), cudaMemcpyDeviceToHost);

		cudaFree(d_bitmapaWe);
		cudaFree(d_bitmapaWy);
		cudaFree(d_size);
		return h_bitmapa;

	}
}
extern "C" {
	__declspec(dllexport) unsigned char* C_Rozjasnianie(unsigned char* h_bitmapa, unsigned long int h_size,int h_wartosc)
	{

		unsigned char *d_bitmapaWe;
		unsigned char *d_bitmapaWy;
		int *d_wartosc=NULL;
		unsigned long int *d_size = NULL;

		cudaMalloc((void**)&d_size, sizeof(unsigned long int));
		cudaMalloc((void**)&d_wartosc, sizeof(int));
		cudaMalloc((void**)&d_bitmapaWe, sizeof(unsigned char)* h_size);
		cudaMalloc((void**)&d_bitmapaWy, sizeof(unsigned char)* h_size);

		cudaMemcpy(d_bitmapaWe, h_bitmapa, h_size * sizeof(unsigned char), cudaMemcpyHostToDevice);
		cudaMemcpy(d_size, &h_size, sizeof(unsigned long int), cudaMemcpyHostToDevice);
		cudaMemcpy(d_wartosc, &h_wartosc, sizeof(int), cudaMemcpyHostToDevice);

		int blocks = (h_size / 256) + 1;
		ProcessPixel_Rozjasnianie << <blocks, 256 >> > (d_bitmapaWe, d_bitmapaWy, d_size,d_wartosc);

		cudaThreadSynchronize();
		cudaMemcpy(h_bitmapa, d_bitmapaWy, h_size * sizeof(unsigned char), cudaMemcpyDeviceToHost);

		cudaFree(d_bitmapaWe);
		cudaFree(d_bitmapaWy);
		cudaFree(d_size);
		return h_bitmapa;

	}
}

int main()
{	
	
	Detect();	unsigned char Bitmapa[50] = {		5,8,9,15,15,15,15,15,15,15, 		15,15,15,15,15,15,15,15,15,15, 		15,15,15,15,15,15,15,15,15,15, 		15,15,15,15,15,15,15,15,15,15, 		15,15,15,15,15,15,15,15,15,15, };	for (int i = 0; i < 50; i++) {
		cout << Bitmapa[i];
	}	C_Negatyw(Bitmapa, 50);
	for (int i = 0; i < 50; i++) {
		cout << Bitmapa[i];
	}
	
    return 0;
}