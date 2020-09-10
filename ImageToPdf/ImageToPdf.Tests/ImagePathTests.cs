using ImageToPdf.Core;
using System;
using System.IO;
using Xunit;

namespace ImageToPdf.Tests
{
    public class ImagePathTests
    {
        [Fact]
        public void ConstructorSuccessJpgTest()
        {
            var testFile = new string(Path.Combine("tests", "imagePathData", "best_girl.jpg"));
            // If the construction fails, it throws and stops the test
            var img = new ImagePath(testFile);
        }

        [Fact]
        public void ConstructorSuccessPngTest()
        {
            var testFile = new string(Path.Combine("tests", "imagePathData", "cat.png"));
            // If the construction fails, it throws and stops the test
            var img = new ImagePath(testFile);
        }

        [Fact]
        public void ConstructorSuccessJpegTest()
        {
            var testFile = new string(Path.Combine("tests", "imagePathData", "cat.jpeg"));
            // If the construction fails, it throws and stops the test
            var img = new ImagePath(testFile);
        }

        [Fact]
        public void ConstructorFailExtensionTest()
        {
            var testFile = new string(Path.Combine("tests", "test_folder", "file.txt"));
            Assert.Throws<ArgumentException>(() =>
                {
                    ImagePath img = new ImagePath(testFile);
                }
                );
        }

        [Fact]
        public void ConstructorFailFileDoesNotExistTest()
        {
            var testFile = new string(Path.Combine("tests", "test_folder", "unknown"));
            Assert.Throws<ArgumentException>(() =>
            {
                ImagePath img = new ImagePath(testFile);
            }
                );
        }
    }
}
