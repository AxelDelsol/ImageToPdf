using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ImageToPdf.Core.ViewModels
{
    public class ImageToPdfViewModel : MvxViewModel
    {
        private ObservableCollection<ImagePath> _queue;
        public ObservableCollection<ImagePath> Queue
        {
            get => _queue;
            set {
                SetProperty(ref _queue, value);
            }
        }

        private ObservableCollection<ImagePath> _done;
        public ObservableCollection<ImagePath> Done
        {
            get => _done;
            set
            {
                SetProperty(ref _done, value);
            }
        }


        private void ResetText()
        {
            
        }
        public IMvxCommand ResetTextCommand => new MvxCommand(ResetText);
        

    }
}
