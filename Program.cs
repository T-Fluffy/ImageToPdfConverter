using System;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Drawing;

namespace ImageToPdfConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: ImageToPdfConverter <inputFolder> <outputPdfFile>");
                return;
            }

            string inputFolder = args[0];
            string outputPdfFile = args[1];

            ConvertToPdf(inputFolder, outputPdfFile);
        }

        static void ConvertToPdf(string inputFolder, string outputPdfFile)
        {
            string[] imageFiles = Directory.GetFiles(inputFolder, "*.png");

            using (PdfDocument pdf = new PdfDocument())
            {
                foreach (string imageFile in imageFiles)
                {
                    using (Bitmap bitmap = new Bitmap(imageFile))
                    {
                        PdfPage page = pdf.AddPage();
                        page.Width = bitmap.Width * 72 / bitmap.HorizontalResolution;
                        page.Height = bitmap.Height * 72 / bitmap.VerticalResolution;
                        XGraphics gfx = XGraphics.FromPdfPage(page);

                        XImage xImage = XImage.FromFile(imageFile);
                        gfx.DrawImage(xImage, 0, 0, page.Width, page.Height);
                    }
                }
                pdf.Save(outputPdfFile);
            }
        }
    }
}
