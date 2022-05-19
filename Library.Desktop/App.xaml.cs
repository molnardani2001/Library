using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using Library.Desktop.Model;
using Library.Desktop.View;
using Library.Desktop.ViewModel;
using Microsoft.Win32;

namespace Library.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private LibraryApiService _service;
        private MainViewModel _mainViewModel;
        private LoginViewModel _loginViewModel;
        private MainWindow _mainWindow;
        private NewBookWindow _newBookWindow;
        private EditRentWindow _editRentWindow;
        private LoginWindow _loginWindow;

        public App()
        {
            Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            _service = new LibraryApiService(ConfigurationManager.AppSettings["baseAddress"]);

            _loginViewModel = new LoginViewModel(_service);

            _loginViewModel.LoginSucceeded += ViewModel_LoginSucceeded;
            _loginViewModel.LoginFailed += ViewModel_LoginFailed;
            _loginViewModel.MessageApplication += MainViewModel_MessageApplication;

            _loginWindow = new LoginWindow
            {
                DataContext = _loginViewModel
            };

            _mainViewModel = new MainViewModel(_service);
            _mainViewModel.LogoutSucceeded += ViewModel_LogoutSucceeded;
            _mainViewModel.MessageApplication += MainViewModel_MessageApplication;
            _mainViewModel.StartingAddBook += MainViewModel_StartingAddBook;
            _mainViewModel.FinishingAddBook += MainViewModel_FinishingAddBook;
            _mainViewModel.StartingRentEdit += MainViewModel_StartingRentEdit;
            _mainViewModel.FinishingRentEdit += MainViewModel_FinishingRentEdit;
            _mainViewModel.StartingSmallImageChange += MainViewModel_StartingSmallImageChange;
            _mainViewModel.StartingNormalImageChange += MainViewModel_StartingNormalImageChange;

            _mainWindow = new MainWindow
            {
                DataContext = _mainViewModel
            };

            MainWindow = _mainWindow;
            ShutdownMode = ShutdownMode.OnMainWindowClose;
            _loginWindow.Closed += LoginView_Closed;

            _loginWindow.Show();
        }

        private void LoginView_Closed(object? sender, EventArgs e)
        {
            Shutdown();
        }

        private void ViewModel_LoginSucceeded(object? sender, EventArgs e)
        {
            _loginWindow.Hide();
            _mainWindow.Show();
        }

        private void ViewModel_LoginFailed(object? sender, EventArgs e)
        {
            MessageBox.Show("Login unsuccessful!", "Library", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void ViewModel_LogoutSucceeded(object? sender, EventArgs e)
        {
            _mainWindow.Hide();
            _loginWindow.Show();
        }

        private void MainViewModel_FinishingAddBook(object? sender, EventArgs e)
        {
            if (_newBookWindow.IsActive)
            {
                _newBookWindow.Close();
            }
        }

        private void MainViewModel_MessageApplication(object? sender, MessageEventArgs e)
        {
            MessageBox.Show(e.Message, "Library", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void MainViewModel_StartingAddBook(object? sender, EventArgs e)
        {
            _newBookWindow = new NewBookWindow
            {
                DataContext = _mainViewModel
            };
            _newBookWindow.ShowDialog();
        }

        private void MainViewModel_StartingRentEdit(object? sender, EventArgs e)
        {
            _editRentWindow = new EditRentWindow
            {
                DataContext = _mainViewModel
            };
            _editRentWindow.ShowDialog();
        }

        private void MainViewModel_FinishingRentEdit(object? sender, EventArgs e)
        {
            if (_editRentWindow.IsActive)
            {
                _editRentWindow.Close();
            }
        }

        private async void MainViewModel_StartingSmallImageChange(object? sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                Filter = "Images|*.jpg;*.jpeg;*.bmp;*.tif;*.gif;*.png",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            };

            if (dialog.ShowDialog(_newBookWindow).GetValueOrDefault(false))
            {
                _mainViewModel.NewBook.SmallCoverImage = await File.ReadAllBytesAsync(dialog.FileName);
            }
        }

        private async void MainViewModel_StartingNormalImageChange(object? sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                Filter = "Images|*.jpg;*.jpeg;*.bmp;*.tif;*.gif;*.png",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            };

            if (dialog.ShowDialog(_newBookWindow).GetValueOrDefault(false))
            {
                _mainViewModel.NewBook.NormalCoverImage = await File.ReadAllBytesAsync(dialog.FileName);
            }
        }
    }
}
