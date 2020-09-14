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
        public List<string> Extensions { get; }

        /// <summary>
        /// Checks that the file exists and ends with a valid extension (.jpg, .jpeg or .png).
        /// </summary>
        /// <exception cref="System.ArgumentException">Thrown when the file does not exist or does not end with a valid extension.</exception>
        /// <param name="fileName">Path to an image.</param>
        public ImagePath(string fileName)
        {
            Extensions = new List<String> { "jpg", "png", "jpeg" };

            // TODO : Be more accurate to know what part of the filename is wrong ?
            if (IsValid(fileName) == false)
                throw new ArgumentException("Invalid file name");

            FileName = fileName;
        }

        private bool IsValid(string fileName)
        {
            return fileName != null && Exists(fileName) && HasValidExtension(fileName);
        }

        private bool HasValidExtension(string fileName)
        {
            return Extensions.Exists(valid_ext => fileName.EndsWith(valid_ext));
        }

        private bool Exists(string fileName)
        {
            return File.Exists(fileName);
        }
    }
}
