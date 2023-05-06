using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace AnalizaObrazu
{

    public struct Rgb
    {
        #region pola
        public byte b, g, r;
        #endregion pola
        public Rgb Negatyw()
        {
            Rgb rob;
            rob.r = (byte)(255 - this.r);
            rob.g = (byte)(255 - this.g);
            rob.b = (byte)(255 - this.b);
            return rob;
        }
        public Rgb Jasnosc()
        {
            Rgb rob;
            rob.r = (byte)(0.299 * this.r + 0.587 * this.g + 0.114 * this.b);
            rob.g = (byte)(0.299 * this.r + 0.587 * this.g + 0.114 * this.b);
            rob.b = (byte)(0.299 * this.r + 0.587 * this.g + 0.114 * this.b);
            return rob;
        }
        public Rgb Rozjasnianie(int wartosc)
        {
            Rgb rob;
            int suma;
            suma = this.r + wartosc;
            if (suma > 255)
                suma = 255;
            if (suma < 0)
                suma = 0;
            rob.r = (byte)suma;
            suma = this.g + wartosc;
            if (suma > 255)
                suma = 255;
            if (suma < 0)
                suma = 0;
            rob.g = (byte)suma;
            suma = this.b + wartosc;
            if (suma > 255)
                suma = 255;
            if (suma < 0)
                suma = 0;
            rob.b = (byte)suma;

            return rob;
        }
    }
    static class Efekty
    {

        public static Bitmap Negatyw(Bitmap bitmapaWe)
        {
            
            int wysokosc = bitmapaWe.Height;
            int szerokosc = bitmapaWe.Width;

            Bitmap bitmapaWy = new Bitmap(szerokosc, wysokosc, PixelFormat.Format24bppRgb);

            BitmapData bmWeData = bitmapaWe.LockBits(new Rectangle(0, 0, szerokosc, wysokosc),ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmWyData = bitmapaWy.LockBits(new Rectangle(0, 0, szerokosc, wysokosc),ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int strideWe = bmWeData.Stride;
            int strideWy = bmWeData.Stride;
            IntPtr scanWe = bmWeData.Scan0;
            IntPtr scanWy = bmWyData.Scan0;
            
            unsafe
            {
                for (int y = 0; y < wysokosc; y++)
                {
                    byte* pWe = (byte*)(void*)scanWe + y * strideWe;
                    byte* pWy = (byte*)(void*)scanWy + y * strideWy;

                    for (int x = 0; x < szerokosc; x++)
                    {
                        ((Rgb*) pWy)[x] = ((Rgb*) pWe)[x].Negatyw();
                    }
                }
            }
            
            bitmapaWy.UnlockBits(bmWyData);
            bitmapaWe.UnlockBits(bmWeData);
           
            return bitmapaWy;
        }
        public static Bitmap Jasnosc(Bitmap bitmapaWe)
        {
            int wysokosc = bitmapaWe.Height;
            int szerokosc = bitmapaWe.Width;

            Bitmap bitmapaWy = new Bitmap(szerokosc, wysokosc, PixelFormat.Format24bppRgb);

            BitmapData bmWeData = bitmapaWe.LockBits(new Rectangle(0, 0, szerokosc, wysokosc), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmWyData = bitmapaWy.LockBits(new Rectangle(0, 0, szerokosc, wysokosc), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int strideWe = bmWeData.Stride;
            int strideWy = bmWeData.Stride;
            IntPtr scanWe = bmWeData.Scan0;
            IntPtr scanWy = bmWyData.Scan0;

            unsafe
            {
                for (int y = 0; y < wysokosc; y++)
                {
                    byte* pWe = (byte*)(void*)scanWe + y * strideWe;
                    byte* pWy = (byte*)(void*)scanWy + y * strideWy;

                    for (int x = 0; x < szerokosc; x++)
                    {
                        ((Rgb*)pWy)[x] = ((Rgb*)pWe)[x].Jasnosc();
                    }
                }
            }
            bitmapaWy.UnlockBits(bmWyData);
            bitmapaWe.UnlockBits(bmWeData);

            return bitmapaWy;
        }
        public static Bitmap Rozjasnianie(Bitmap bitmapaWe, int wartosc)
        {
            int wysokosc = bitmapaWe.Height;
            int szerokosc = bitmapaWe.Width;

            Bitmap bitmapaWy = new Bitmap(szerokosc, wysokosc, PixelFormat.Format24bppRgb);

            BitmapData bmWeData = bitmapaWe.LockBits(new Rectangle(0, 0, szerokosc, wysokosc), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmWyData = bitmapaWy.LockBits(new Rectangle(0, 0, szerokosc, wysokosc), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int strideWe = bmWeData.Stride;
            int strideWy = bmWeData.Stride;
            IntPtr scanWe = bmWeData.Scan0;
            IntPtr scanWy = bmWyData.Scan0;

            unsafe
            {
                for (int y = 0; y < wysokosc; y++)
                {
                    byte* pWe = (byte*)(void*)scanWe + y * strideWe;
                    byte* pWy = (byte*)(void*)scanWy + y * strideWy;

                    for (int x = 0; x < szerokosc; x++)
                    {
                        ((Rgb*)pWy)[x] = ((Rgb*)pWe)[x].Rozjasnianie(wartosc);
                    }
                }
            }
            bitmapaWy.UnlockBits(bmWyData);
            bitmapaWe.UnlockBits(bmWeData);

            return bitmapaWy;
        }
        public unsafe static Bitmap CNegatyw(Bitmap bitmapaWe)
        {
            int wysokosc = bitmapaWe.Height;
            int szerokosc = bitmapaWe.Width;

            byte[] Bitmapa = ImageToByte(bitmapaWe);

            ulong size = (ulong)(3 * wysokosc *szerokosc);

            fixed (byte* pBitmapa = Bitmapa)
            {

                Bitmap bitmapaWy = new Bitmap(szerokosc, wysokosc, PixelFormat.Format24bppRgb);
                fixed (byte* inBuf = Bitmapa)
                {
                    byte* outBuf = Program.C_Negatyw(inBuf, size);
                }
                    BitmapData bmData = bitmapaWy.LockBits(new System.Drawing.Rectangle(0, 0, szerokosc, wysokosc), ImageLockMode.ReadWrite,PixelFormat.Format24bppRgb);
                    IntPtr pNative = bmData.Scan0;
                    Marshal.Copy(Bitmapa, 0, pNative, (int)size);
                    bitmapaWy.UnlockBits(bmData);
                return bitmapaWy;
            }
        }
        public unsafe static Bitmap CJasnosc(Bitmap bitmapaWe)
        {
            int wysokosc = bitmapaWe.Height;
            int szerokosc = bitmapaWe.Width;

            byte[] Bitmapa = ImageToByte(bitmapaWe);

            ulong size = (ulong)(3 * wysokosc * szerokosc);

            fixed (byte* pBitmapa = Bitmapa)
            {

                Bitmap bitmapaWy = new Bitmap(szerokosc, wysokosc, PixelFormat.Format24bppRgb);
                fixed (byte* inBuf = Bitmapa)
                {
                    byte* outBuf = Program.C_Jasnosc(inBuf, size);
                }
                BitmapData bmData = bitmapaWy.LockBits(new System.Drawing.Rectangle(0, 0, szerokosc, wysokosc), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                IntPtr pNative = bmData.Scan0;
                Marshal.Copy(Bitmapa, 0, pNative, (int)size);
                bitmapaWy.UnlockBits(bmData);
                return bitmapaWy;
            }
        }
        public unsafe static Bitmap CRozjasnianie(Bitmap bitmapaWe,int wartosc)
        {
            int wysokosc = bitmapaWe.Height;
            int szerokosc = bitmapaWe.Width;

            byte[] Bitmapa = ImageToByte(bitmapaWe);

            ulong size = (ulong)(3*wysokosc * szerokosc);

            fixed (byte* pBitmapa = Bitmapa)
            {

                Bitmap bitmapaWy = new Bitmap(szerokosc, wysokosc, PixelFormat.Format24bppRgb);
                fixed (byte* inBuf = Bitmapa)
                {
                    byte* outBuf = Program.C_Rozjasnianie(inBuf, size,wartosc);
                }
                BitmapData bmData = bitmapaWy.LockBits(new System.Drawing.Rectangle(0, 0, szerokosc, wysokosc), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                IntPtr pNative = bmData.Scan0;
                Marshal.Copy(Bitmapa, 0, pNative, (int)size);
                bitmapaWy.UnlockBits(bmData);
                return bitmapaWy;
            }
        }
        public unsafe static byte[] ImageToByte(Bitmap bitmapa)
        {
            
            byte[] TablicaBytow;
            TablicaBytow =new byte[3*bitmapa.Height * bitmapa.Width];
            int wysokosc = bitmapa.Height;
            int szerokosc = bitmapa.Width;

            BitmapData bmWeData = bitmapa.LockBits(new Rectangle(0, 0, szerokosc, wysokosc), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int strideWe = bmWeData.Stride;
            IntPtr scanWe = bmWeData.Scan0;
            int n = 0;
            for (int y = 0; y <wysokosc; y++)
            {
                byte* pWe = (byte*)(void*)scanWe +y * strideWe;

                for (int x = 0; x <3*szerokosc; x++)
                {
                    TablicaBytow[n] = (pWe)[x];
                    n++;
                }
            }
            bitmapa.UnlockBits(bmWeData);
            return TablicaBytow;
            
        }

    }
}
