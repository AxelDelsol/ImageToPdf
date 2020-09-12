using ImageToPdf.Core.Services;
using ImageToPdf.Core.ViewModels;
using MvvmCross;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageToPdf.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.IoCProvider.RegisterType<IConverter, PdfConverter>();
            RegisterAppStart<ImageToPdfViewModel>();
        }
    }
}
