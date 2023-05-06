using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;



namespace AnalizaObrazu
{
    static unsafe class Program
    {
        [DllImport("CudaHelloWorld.dll", CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "Detect", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern void Detect();
        [DllImport("CudaHelloWorld.dll", CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "C_Negatyw", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
              public static extern byte* C_Negatyw(byte* Bitmapa,ulong size);
        [DllImport("CudaHelloWorld.dll", CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "C_Jasnosc", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern byte* C_Jasnosc(byte* Bitmapa, ulong size);
        [DllImport("CudaHelloWorld.dll", CharSet = CharSet.Ansi, SetLastError = true, EntryPoint = "C_Rozjasnianie", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern byte* C_Rozjasnianie(byte* Bitmapa, ulong size,int wartosc);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Console.Write("Launching Cuda DLL\n");
                Detect();
            }
               catch (DllNotFoundException)
            {
            Console.Write("Missing CUDA DLL\n");
               }
            catch (EntryPointNotFoundException e)
            {
                Console.WriteLine("{0}:  {1}", e.GetType().Name,
                         e.Message);
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());



        

        }
    }
}
