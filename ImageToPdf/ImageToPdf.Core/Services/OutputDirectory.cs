﻿using System;
using System.IO;

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
        /// <exception cref="ArgumentNullException">Thrown when directoryName is null.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">Thrown when the directory does not exist.</exception>
        /// <param name="directoryName">Path of the directory</param>
        public OutputDirectory(string directoryName)
        {
            Validate(directoryName);

            DirectoryName = directoryName;
        }

        /// <summary>
        /// Validate the input string
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when directoryName is null.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">Thrown when the directory does not exist.</exception>
        /// <param name="directoryName"></param>
        private void Validate(string directoryName)
        {
            _ = directoryName ?? throw ExceptionHelper.GetArgumentNullException();

            if (Directory.Exists(directoryName) == false)
                throw ExceptionHelper.GetDirectoryNotFoundException(directoryName);
        }
    }
}
