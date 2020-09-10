using System;
using System.Collections.Generic;
using System.IO;

namespace ImageToPdf.Core
{
    /// <summary>
    /// This class is used to store an immutable path to a image file.
    /// </summary>
    public class ImagePath
    {
        public string FileName { get; }

        public List<string> Extensions { get; }

        public ImagePath(string filename)
        {
            Extensions = new List<String> { "jpg", "png", "jpeg" };

            // TODO : Be more accurate to know what part of the filename is wrong ?
            if (IsValid(filename) == false)
                throw new ArgumentException("Invalid filename");

            FileName = filename;
        }

        private bool IsValid(string filename)
        {
            return filename != null && Exists(filename) && ValidExtension(filename);
        }

        private bool ValidExtension(string filename)
        {
            return Extensions.Exists(valid_ext => filename.EndsWith(valid_ext));
        }

        private bool Exists(string filename)
        {
            return File.Exists(filename);
        }
    }
}
