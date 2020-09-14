using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ImageToPdf.Core.Services
{
    /// <summary>
    /// Implementation of IConverter.
    /// </summary>
    public class PdfConverter : IConverter
    {
        /// <summary>
        /// Add the image in the document in a new page.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="document"></param>
        public void ToPdf(ImagePath image, PdfDocument document)
        {
            var img = XImage.FromFile(image.FileName);
            PdfPage page = document.AddPage();
            page.Width = img.PointWidth;
            page.Height = img.PointHeight;
            var gfx = XGraphics.FromPdfPage(page);
            gfx.DrawImage(img, 0, 0, (int)page.Width, (int)page.Height);
        }

        public void Convert(IEnumerable<ImagePath> images, OutputDirectory outputDirectory)
        {
            foreach (var img in images)
            {
                var document = new PdfDocument();
                ToPdf(img, document);
                string outputName = GetOutputName(img, outputDirectory);
                document.Save(outputName);
            }
        }

        public void Merge(IEnumerable<ImagePath> images, string outputFileName)
        {
            var document = new PdfDocument();

            foreach (var img in images)
            {
                ToPdf(img, document);
            }

            document.Save(outputFileName);
        }

        private string GetOutputName(ImagePath img, OutputDirectory outputDirectory)
        {
            return Path.Combine(
                outputDirectory.DirectoryName,
                Path.GetFileNameWithoutExtension(img.FileName) + ".pdf"
            );
        }
    }
}
