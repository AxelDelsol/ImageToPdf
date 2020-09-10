using System;
using System.Collections.Generic;
using System.Text;

namespace ImageToPdf.Core.Services
{
    public interface IConverter
    {
        /// <summary>
        /// For each img in images, create a pdf file in outDir.
        /// If img.FileName = "test.png", thea associated pdf file will be outDir\test.pdf
        /// </summary>
        /// <param name="images">Collections of images to convertt</param>
        /// <param name="outDir"> Output directory</param>
        void Convert(IEnumerable<ImagePath> images, OutputDirectory outDir);

        /// <summary>
        /// Merges all images into one pdf called outputName.
        /// Create a specific type for the output ?
        /// </summary>
        /// <param name="images"></param>
        /// <param name="outputName"></param>
        void Merge(IEnumerable<ImagePath> images, string outputName);
    }
}
