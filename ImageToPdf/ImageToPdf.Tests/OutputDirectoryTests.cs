using ImageToPdf.Core;
using ImageToPdf.Core.Services;
using System;
using System.IO;
using Xunit;

namespace ImageToPdf.Tests
{
    public class OutputDirectoryTests
    {
        [Fact]
        public void ConstructorSuccessTest1()
        {
            var testDir = Directory.GetCurrentDirectory();
            // If the construction fails, it throws and stops the test
            var dir = new OutputDirectory(testDir);
        }

        [Fact]
        public void ConstructorSuccessTest2()
        {
            var testDir = "tests";
            // If the construction fails, it throws and stops the test
            var dir = new OutputDirectory(testDir);
        }

        [Fact]
        public void ConstructorSuccessTest3()
        {
            var testDir = Path.Combine("tests", "test_folder");
            // If the construction fails, it throws and stops the test
            var dir = new OutputDirectory(testDir);
        }

        [Fact]
        public void ConstructorFailTest1()
        {
            var testDir = Path.Combine("tests", "test_folder", "file.txt");

            Assert.Throws<DirectoryNotFoundException>(() =>
            {
                var dir = new OutputDirectory(testDir);
            });
        }

        [Fact]
        public void ConstructorFailTest2()
        {
            var testDir = "unknown";

            Assert.Throws<DirectoryNotFoundException>(() =>
            {
                var dir = new OutputDirectory(testDir);
            });
        }

        [Fact]
        public void ConstructorFailTest3()
        {
            string testDir = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                var dir = new OutputDirectory(testDir);
            });
        }
    }
}
