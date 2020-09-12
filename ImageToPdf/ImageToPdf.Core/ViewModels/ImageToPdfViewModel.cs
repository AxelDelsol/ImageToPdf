using ImageToPdf.Core.Services;
using Microsoft.Win32;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace ImageToPdf.Core.ViewModels
{
    public class ImageToPdfViewModel : MvxViewModel
    {
        private IConverter _converter;

        private ObservableCollection<ImagePath> _queue = new ObservableCollection<ImagePath>();
        public ObservableCollection<ImagePath> Queue
        {
            get => _queue;
            set {
                SetProperty(ref _queue, value);
            }
        }

        private ObservableCollection<ImagePath> _done = new ObservableCollection<ImagePath>();
        public ObservableCollection<ImagePath> Done
        {
            get => _done;
            set
            {
                SetProperty(ref _done, value);
            }
        }

        public ImageToPdfViewModel(IConverter converter)
        {
            _converter = converter;
        }


        private void AddImages()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog 
            { Multiselect = true,
              Filter = "Image files(*.png; *.jpeg; *.jpg)| *.png; *.jpeg; *.jpg"
            };
            
            if (openFileDialog.ShowDialog() == true)
            {
                var files = openFileDialog.FileNames;
                foreach (var file in files)
                {
                    ImagePath img = new ImagePath(file);
                    _queue.Add(img);
                }
            }
        }
        public IMvxCommand AddImagesCommand => new MvxCommand(AddImages);

        private void Clear()
        {
            _queue.Clear();
            _done.Clear();
        }
        public IMvxCommand ClearCommand => new MvxCommand(Clear);

        private void Convert()
        {
            string outStr = SelectDirectory();
            OutputDirectory outDir = new OutputDirectory(outStr);
            _converter.Convert(_queue, outDir);
            MoveQueueToDone();
        }
        public IMvxCommand ConvertCommand => new MvxCommand(Convert);

        private void Merge()
        {
            string outStr = SelectOutputFile();
            _converter.Merge(_queue, outStr);
            MoveQueueToDone();
        }
        public IMvxCommand MergeCommand => new MvxCommand(Merge);

        private void MoveQueueToDone()
        {
            foreach(var img in _queue)
            {
                _done.Add(img);
            }

            _queue.Clear();
        }

        private string SelectDirectory()
        {
            VistaFolderBrowserDialog folderDialog = new VistaFolderBrowserDialog();
            folderDialog.ShowDialog();
            return folderDialog.SelectedPath;
        }

        private string SelectOutputFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "pdf";
            saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
            saveFileDialog.ShowDialog();
            return saveFileDialog.FileName;
        }
    }
}
