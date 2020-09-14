using System.Collections.Generic;

namespace ImageToPdf.Core.Services
{
    public interface IConverter
    {
        /// <summary>
        /// For each img in images, create a pdf file in outputDirectory.
        /// If img.FileName = "test.png", the associated pdf file will be outDir\test.pdf.
        /// </summary>
        /// <param name="images">Collections of images to convert.</param>
        /// <param name="outputDirectory">Directory containing the converted images.</param>
        void Convert(IEnumerable<ImagePath> images, OutputDirectory outputDirectory);

        /// <summary>
        /// Merges all images into one pdf called outputName.
        /// Create a specific type for the output ?
        /// </summary>
        /// <param name="images">Collections of images to convert.</param>
        /// <param name="outputFileName">Pdf File containing the merged images.</param>
        void Merge(IEnumerable<ImagePath> images, string outputFileName);
    }
}
