using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Policy;
using System.Text;
using System.Windows.Navigation;

namespace ImageToPdf.Core.Services
{
    /// <summary>
    /// This helper class allows to have uniform exception messages across the project
    /// </summary>
    public static class ExceptionHelper
    {
        public static ArgumentNullException GetArgumentNullException() =>
            new ArgumentNullException("Can not pass null as a parameter.");

        public static FileNotFoundException GetFileNotFoundException(string fileName) =>
            new FileNotFoundException($"Could not find file : {fileName}.");

        public static ArgumentException GetArgumentException(string arg) =>
            new ArgumentException($"Invalid argument : {arg}.");

        public static DirectoryNotFoundException GetDirectoryNotFoundException(string directoryName) =>
            new DirectoryNotFoundException($"Could not find directory : {directoryName}.");
    }
}
