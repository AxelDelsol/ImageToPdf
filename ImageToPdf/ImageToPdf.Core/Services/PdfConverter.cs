using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ImageToPdf.Core.Services
{
    public class PdfConverter : IConverter
    {
        /// <summary>
        /// Add the image in the document in a new page
        /// </summary>
        /// <param name="image"></param>
        /// <param name="document"></param>
        public void ToPdf(ImagePath image, PdfDocument document)
        {
            XImage img = XImage.FromFile(image.FileName);
            PdfPage page = document.AddPage();
            page.Width = img.PointWidth;
            page.Height = img.PointHeight;
            XGraphics gfx = XGraphics.FromPdfPage(page);
            gfx.DrawImage(img, 0, 0, (int)page.Width, (int)page.Height);
        }

        public void Convert(IEnumerable<ImagePath> images, OutputDirectory outDir)
        {
            foreach (var img in images)
            {
                PdfDocument document = new PdfDocument();
                ToPdf(img, document);
                string outputName = GetOutputName(img, outDir);
                document.Save(outputName);
            }
        }

        public void Merge(IEnumerable<ImagePath> images, string outputName)
        {
            PdfDocument document = new PdfDocument();

            foreach (var img in images)
            {
                ToPdf(img, document);
            }

            document.Save(outputName);
        }

        private string GetOutputName(ImagePath img, OutputDirectory outDir)
        {
            return Path.Combine(
                outDir.DirName,
                Path.GetFileNameWithoutExtension(img.FileName) + ".pdf"
            );
        }
    }
}
