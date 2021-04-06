using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace JPEG.Images
{
    class Matrix
    {
        public readonly int Height;
        public readonly Pixel[,] Pixels;
        public readonly int Width;

        public Matrix(int height, int width)
        {
            Height = height;
            Width = width;

            Pixels = new Pixel[height, width];


            // Parallel.For(0, width, j =>
            // {
            //     for (var i = 0; i < height; ++i)
            //         Pixels[i, j] = new Pixel(0, 0, 0, PixelFormat.RGB);
            // });
        }

        public static explicit operator Matrix(Bitmap bmp)
        {
            // var height = bmp.Height - bmp.Height % 8;
            // var width = bmp.Width - bmp.Width % 8;
            // var matrix = new Matrix(height, width);
            // var data = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size), ImageLockMode.ReadWrite,
            //     System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            // var rowPtr = (byte*) data.Scan0;
            // for (var j = 0; j < height; j++)
            // {
            //     var pixelPtr = rowPtr;
            //     for (var i = 0; i < width; i++)
            //     {
            //         var r = *pixelPtr;
            //         pixelPtr++;
            //         var g = *pixelPtr;
            //         pixelPtr++;
            //         var b = *pixelPtr;
            //         pixelPtr++;
            //         matrix.Pixels[j, i] = new Pixel(b, g, r, PixelFormat.RGB);
            //     }
            //
            //     rowPtr += data.Stride;
            // }
            //
            // bmp.UnlockBits(data);
            // return matrix;
            var height = bmp.Height - bmp.Height % 8;
            var width = bmp.Width - bmp.Width % 8;
            var matrix = new Matrix(height, width);

            for (var j = 0; j < height; j++)
            {
                for (var i = 0; i < width; i++)
                {
                    var pixel = bmp.GetPixel(i, j);
                    matrix.Pixels[j, i] = new Pixel(pixel.R, pixel.G, pixel.B, PixelFormat.RGB);
                }
            }

            return matrix;
        }

        public static explicit operator Bitmap(Matrix matrix)
        {
            var bmp = new Bitmap(matrix.Width, matrix.Height);
            // var data = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size), ImageLockMode.ReadOnly,
            // System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            // var rowPtr = (byte*) data.Scan0;
            for (var j = 0; j < bmp.Height; j++)
            {
                // var pixelPtr = rowPtr;
                for (var i = 0; i < bmp.Width; i++)
                {
                    var pixel = matrix.Pixels[j, i];
                    // *(pixelPtr + i) = ToByte(pixel.R);
                    // pixelPtr++;
                    // *(pixelPtr + i) = ToByte(pixel.G);
                    // pixelPtr++;
                    // *(pixelPtr + i) = ToByte(pixel.B);
                    // pixelPtr++;
                    bmp.SetPixel(i, j, Color.FromArgb(ToByte(pixel.R), ToByte(pixel.G), ToByte(pixel.B)));
                }

                // rowPtr += data.Stride;
            }

            // bmp.UnlockBits(data);
            return bmp;
        }


        public static byte ToByte(double d)
        {
            var val = (int) d;
            if (val > byte.MaxValue)
                return byte.MaxValue;
            if (val < byte.MinValue)
                return byte.MinValue;
            return (byte) val;
        }
    }
}