using ImageToPdf.Core.Services;
using Microsoft.Win32;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Ookii.Dialogs.Wpf;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace ImageToPdf.Core.ViewModels
{
    public class ImageToPdfViewModel : MvxViewModel
    {
        // Data 
        private ObservableCollection<ImagePath> _queue;
        private ObservableCollection<ImagePath> _done;

        // Converter
        private readonly IConverter _converter;


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

        public IMvxCommand AddImagesCommand { get; }

        public IMvxCommand ClearCommand { get; }

        public IMvxCommand ConvertCommand { get; }

        public IMvxCommand MergeCommand { get; }


        // Constructor
        public ImageToPdfViewModel(IConverter converter)
        {
            _ = converter ?? throw ExceptionHelper.GetArgumentNullException();

            _queue = new ObservableCollection<ImagePath>();
            _done = new ObservableCollection<ImagePath>();

            _converter = converter;

            AddImagesCommand = new MvxCommand(AddImages);
            ClearCommand = new MvxCommand(Clear);
            ConvertCommand = new MvxCommand(Convert);
            MergeCommand = new MvxCommand(Merge);
        }

        // Private
        private void AddImages()
        {
            var openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "Image files(*.png; *.jpeg; *.jpg)| *.png; *.jpeg; *.jpg"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (var file in openFileDialog.FileNames)
                {
                    try
                    {
                        var img = new ImagePath(file);
                        _queue.Add(img);
                    }
                    catch (ArgumentNullException e)
                    {
                        MessageBox.Show(e.Message);
                    }
                    catch (FileNotFoundException e)
                    {
                        MessageBox.Show($"{e.Message}.\n Please, check that the file exists.");
                    }
                    catch (ArgumentException e)
                    {
                        MessageBox.Show($"{e.Message}.\n Valid extensions : png, jpeg, jpg.");
                    }

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
            OutputDirectory? outputDirectory = SelectOutputDirectory();

            if (outputDirectory != null)
            {
                _converter.Convert(_queue, outputDirectory);
                MoveQueueToDone();
            }
            
        }

        private void Merge()
        {
            string? outputFile = SelectOutputFile();

            if (outputFile != null)
            {
                _converter.Merge(_queue, outputFile);
                MoveQueueToDone();
            }
            
        }

        private void MoveQueueToDone()
        {
            foreach (var img in _queue)
            {
                _done.Add(img);
            }

            _queue.Clear();
        }

        private OutputDirectory? SelectOutputDirectory()
        {
            var folderDialog = new VistaFolderBrowserDialog();

            if (folderDialog.ShowDialog() == true)
                return new OutputDirectory(folderDialog.SelectedPath);

            return null;
        }

        private string? SelectOutputFile()
        {
            var saveFileDialog = new SaveFileDialog()
            {
                DefaultExt = "pdf",
                Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*"
            };

            if (saveFileDialog.ShowDialog() == true)
                return saveFileDialog.FileName;

            return null;
        }
    }
}
