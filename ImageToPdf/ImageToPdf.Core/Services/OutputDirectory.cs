using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ImageToPdf.Core.Services
{
    public class OutputDirectory
    {
        public string DirName { get; }
        public OutputDirectory(string dirname)
        {
            if (Directory.Exists(dirname) == false)
                throw new DirectoryNotFoundException("Invalid filename");

            DirName = dirname;
        }
    }
}
