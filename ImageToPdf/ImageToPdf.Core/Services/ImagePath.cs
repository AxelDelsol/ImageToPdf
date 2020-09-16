using ImageToPdf.Core.Services;
using System;
using System.Collections.Generic;
using System.IO;

namespace ImageToPdf.Core
{
    /// <summary>
    /// This class is used to store an immutable path to a image file.
    /// At construction, it checks that the file exists and ends with a valid extension (.jpg, .jpeg or .png).
    /// </summary>
    public class ImagePath
    {
        public string FileName { get; }
        public static List<string> Extensions { get; }

        /// <summary>
        /// Checks that the file exists and ends with a valid extension (.jpg, .jpeg or .png).
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when fileName is null.</exception>
        /// <exception cref="FileNotFoundException">Thrown when fileName can not be found on the disk.</exception>
        /// <exception cref="ArgumentException">Thrown when fileName does not have a valid extension.</exception>
        /// <param name="fileName">Path to an image.</param>
        public ImagePath(string fileName)
        {


            Validate(fileName);

            FileName = fileName;
        }

        static ImagePath()
        {
            Extensions = new List<String> { "jpg", "png", "jpeg" };
        }

        /// <summary>
        /// Validate the input string
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when fileName is null.</exception>
        /// <exception cref="FileNotFoundException">Thrown when fileName can not be found on the disk.</exception>
        /// <exception cref="ArgumentException">Thrown when fileName does not have a valid extension.</exception>
        /// <param name="fileName"></param>
        private void Validate(string fileName)
        {
            _ = fileName ?? throw ExceptionHelper.GetArgumentNullException();

            if (Exists(fileName) == false)
                throw ExceptionHelper.GetFileNotFoundException(fileName);

            if (HasValidExtension(fileName) == false)
                throw ExceptionHelper.GetArgumentException(fileName);

        }

        private bool HasValidExtension(string fileName)
        {
            return Extensions.Exists(
                valid_ext => fileName.EndsWith(valid_ext, StringComparison.CurrentCultureIgnoreCase)
            );
        }

        private bool Exists(string fileName)
        {
            return File.Exists(fileName);
        }
    }
}
