using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ImageToPdf.Core.Services
{
    public class OutputDirectory
    {
        /// <summary>
        /// This class is used to store an immutable path to a directory.
        /// At construction, it checks that the directory exists.
        /// </summary>
        public string DirectoryName { get; }

        /// <summary>
        /// Checks that the directory exists.
        /// </summary>
        /// <exception cref="System.IO.DirectoryNotFoundException">Thrown when the directory does not exist.</exception>
        /// <param name="directoryName">Path of the directory</param>
        public OutputDirectory(string directoryName)
        {
            if (Directory.Exists(directoryName) == false)
                throw new DirectoryNotFoundException("Invalid filename");

            DirectoryName = directoryName;
        }
    }
}
