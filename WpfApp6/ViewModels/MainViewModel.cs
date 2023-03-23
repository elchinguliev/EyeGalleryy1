using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp6.Views;
using System.Windows.Forms;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows;

namespace WpfApp6.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        public ObservableCollection<string>? Paths { get; set; }
        public string CurrentPath { get; set; } = string.Empty;

        private Page? currentPage;

        public Page? CurrentPage
        {
            get { return currentPage; }
            set { Set(ref currentPage, value); }
        }
        public MainViewModel()
        {
            Paths = new();
            CurrentPage = new PhotosPage { DataContext = this };
        }


        public RelayCommand? OpenFolderCommand
        {
            get => new RelayCommand(() =>
            {
                using FolderBrowserDialog fbd = new();
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    CurrentPath = fbd.SelectedPath;
                    Paths?.Clear();
                    var paths = Directory.GetFiles(CurrentPath);
                    foreach (var path in paths)
                    {
                        if (path.EndsWith(".png") || path.EndsWith(".jpg") || path.EndsWith(".jpeg"))
                            Paths?.Add(path);
                    }
                }
            });
        }

        public RelayCommand? AddFileCommand
        {
            get => new RelayCommand(() =>
            {
                using var ofd = new OpenFileDialog();
                ofd.Filter = "Photo|*.png;*.jpg;*.jpeg";
                var result = ofd.ShowDialog();
                if (result == DialogResult.OK)
                    Paths?.Add(ofd.FileName);
            });
        }
        public RelayCommand? ExitCommand
        {
            get => new RelayCommand(() =>
            {
                App.Current.Shutdown();


            });
        }
    }
}
