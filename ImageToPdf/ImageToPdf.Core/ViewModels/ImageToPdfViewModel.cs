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
        // Data 
        private ObservableCollection<ImagePath> _queue = new ObservableCollection<ImagePath>();
        private ObservableCollection<ImagePath> _done = new ObservableCollection<ImagePath>();

        // Converter
        private IConverter _converter;

        // Commands
        private IMvxCommand _addImagesCommand;
        private IMvxCommand _clearCommand;
        private IMvxCommand _convertCommand;
        private IMvxCommand _mergeCommand;


        // Public properties
        public ObservableCollection<ImagePath> Queue
        {
            get => _queue;
            set => SetProperty(ref _queue, value);
        }

        public ObservableCollection<ImagePath> Done
        {
            get => _done;
            set => SetProperty(ref _done, value);
        }

        public IMvxCommand AddImagesCommand => _addImagesCommand;

        public IMvxCommand ClearCommand => _clearCommand;

        public IMvxCommand ConvertCommand => _convertCommand;

        public IMvxCommand MergeCommand => _mergeCommand;


        // Constructor
        public ImageToPdfViewModel(IConverter converter)
        {
            _queue = new ObservableCollection<ImagePath>();
            _done = new ObservableCollection<ImagePath>();

            _converter = converter;

            _addImagesCommand = new MvxCommand(AddImages);
            _clearCommand = new MvxCommand(Clear);
            _convertCommand = new MvxCommand(Convert);
            _mergeCommand = new MvxCommand(Merge);
        }

        // Private
        private void AddImages()
        {
            var openFileDialog = new OpenFileDialog 
            { Multiselect = true,
              Filter = "Image files(*.png; *.jpeg; *.jpg)| *.png; *.jpeg; *.jpg"
            };
            
            if (openFileDialog.ShowDialog() == true)
            {
                var files = openFileDialog.FileNames;
                foreach (var file in files)
                {
                    var img = new ImagePath(file);
                    _queue.Add(img);
                }
            }
        }

        private void Clear()
        {
            _queue.Clear();
            _done.Clear();
        }
        
        private void Convert()
        {
            OutputDirectory outputDirectory = SelectOutputDirectory();
            _converter.Convert(_queue, outputDirectory);
            MoveQueueToDone();
        }
        
        private void Merge()
        {
            string outputFile = SelectOutputFile();
            _converter.Merge(_queue, outputFile);
            MoveQueueToDone();
        }
        
        private void MoveQueueToDone()
        {
            foreach(var img in _queue)
            {
                _done.Add(img);
            }

            _queue.Clear();
        }

        private OutputDirectory SelectOutputDirectory()
        {
            var folderDialog = new VistaFolderBrowserDialog();
            folderDialog.ShowDialog();
            return new OutputDirectory(folderDialog.SelectedPath);
        }

        private string SelectOutputFile()
        {
            var saveFileDialog = new SaveFileDialog()
            {
                DefaultExt = "pdf",
                Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*"
            };

            saveFileDialog.ShowDialog();
            return saveFileDialog.FileName;
        }
    }
}
