using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Library.Data;
using Library.Desktop.Model;

namespace Library.Desktop.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly LibraryApiService _service;
        private ObservableCollection<BookViewModel> _books;
        private ObservableCollection<VolumeViewModel> _volumes;
        private ObservableCollection<RentViewModel> _rents;
        private BookViewModel _selectedBook;
        private VolumeViewModel _selectedVolume;
        private RentViewModel _selectedRent;
        private BookViewModel _newBook;
        private RentViewModel _editableRent;

        public ObservableCollection<BookViewModel> Books
        {
            get => _books;
            set
            {
                _books = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<VolumeViewModel> Volumes
        {
            get => _volumes;
            set
            {
                _volumes = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<RentViewModel> Rents
        {
            get => _rents;
            set
            {
                _rents = value;
                OnPropertyChanged();
            }
        }

        public BookViewModel SelectedBook
        {
            get => _selectedBook;
            set
            {
                _selectedBook = value;
                OnPropertyChanged();
            }
        }

        public VolumeViewModel SelectedVolume
        {
            get => _selectedVolume;
            set
            {
                _selectedVolume = value;
                OnPropertyChanged();
            }
        }

        public RentViewModel SelectedRent
        {
            get => _selectedRent;
            set
            {
                _selectedRent = value;
                OnPropertyChanged();
            }
        }

        public BookViewModel NewBook
        {
            get => _newBook;
            set
            {
                _newBook = value;
                OnPropertyChanged();
            }
        }

        public RentViewModel EditableRent
        {
            get => _editableRent;
            set
            {
                _editableRent = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand RefreshBooksCommand { get; private set; }

        public DelegateCommand LogoutCommand { get; private set; }

        public DelegateCommand SelectBookCommand { get; private set; }

        public DelegateCommand SelectVolumeCommand { get; private set; }

        public DelegateCommand AddBookCommand { get; private set; }

        public DelegateCommand AddVolumeCommand { get; private set; }

        public DelegateCommand DeleteVolumeCommand { get; private set; }

        public DelegateCommand EditRentCommand { get; private set; }

        public DelegateCommand ChangeSmallImageCommand { get; private set; }

        public DelegateCommand ChangeNormalImageCommand { get; private set; }

        public DelegateCommand CancelAddBookCommand { get; private set; }

        public DelegateCommand SaveBookCommand { get; private set; }

        public DelegateCommand CancelEditRentCommand { get; private set; }

        public DelegateCommand SaveRentCommand { get; private set; }

        public event EventHandler LogoutSucceeded;

        public event EventHandler StartingRentEdit;

        public event EventHandler FinishingRentEdit;

        public event EventHandler StartingAddBook;

        public event EventHandler FinishingAddBook;

        public event EventHandler StartingSmallImageChange;

        public event EventHandler StartingNormalImageChange;

        public MainViewModel(LibraryApiService service)
        {
            _service = service;

            //könyvek
            RefreshBooksCommand = new DelegateCommand(_ => LoadBooksAsync());
            SelectBookCommand = new DelegateCommand(_ => LoadVolumesAsync(SelectedBook));
            LogoutCommand = new DelegateCommand(_ => LogoutAsync());

            AddBookCommand = new DelegateCommand(_ => StartAddBook());

            //kötetek
            AddVolumeCommand = new DelegateCommand(_ => !(SelectedBook is null),_ => AddVolumeAsync());
            DeleteVolumeCommand =
                new DelegateCommand(_ => !(SelectedVolume is null), _ => DeleteVolumeAsync(SelectedVolume));
            SelectVolumeCommand = new DelegateCommand(_ => LoadRentsAsync(SelectedVolume));

            //kölcsönzések
            EditRentCommand = new DelegateCommand(_ => !(SelectedRent is null), _ => StartEditRent());

            //új könyv hozzáadása
            CancelAddBookCommand = new DelegateCommand(_ => CancelAddBook());
            SaveBookCommand = new DelegateCommand(_ => SaveBookAsync());
            ChangeSmallImageCommand = new DelegateCommand(_ => StartingSmallImageChange?.Invoke(this, EventArgs.Empty));
            ChangeNormalImageCommand =
                new DelegateCommand(_ => StartingNormalImageChange?.Invoke(this, EventArgs.Empty));

            //foglalás módosítása
            CancelEditRentCommand = new DelegateCommand(_ => CancelRentEdit());
            SaveRentCommand = new DelegateCommand(_ => SaveRentEditAsync());
        }

        private async void LogoutAsync()
        {
            try
            {
                await _service.LogoutAsync();
                LogoutSucceeded?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occurred! ({ex.Message})");
            }
        }

        private async void LoadBooksAsync()
        {
            try
            {
                Books = new ObservableCollection<BookViewModel>((await _service.LoadBooksAsync())
                    .Select(book => (BookViewModel)book));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occurred! ({ex.Message})");
            }
        }

        private void StartAddBook()
        {
            NewBook = new BookViewModel();
            NewBook.PopularityNumber = 0;
            NewBook.Year = 2000;
            StartingAddBook?.Invoke(this, EventArgs.Empty);
        }

        private void CancelAddBook()
        {
            NewBook = null;
            FinishingAddBook?.Invoke(this, EventArgs.Empty);
        }

        private async void SaveBookAsync()
        {
            try
            {
                await _service.CreateBookAsync((BookDTO)NewBook);
                LoadBooksAsync();
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occurred! ({ex.Message})");
            }
            FinishingAddBook?.Invoke(this,EventArgs.Empty);
        }

        #region Volumes

        private async void LoadVolumesAsync(BookViewModel book)
        {
            if (book is null)
            {
                Volumes = null;
                Rents = null;
                return;
            }

            try
            {
                Volumes = new ObservableCollection<VolumeViewModel>((await _service.LoadVolumesAsync(book.Id))
                    .Select(volume => (VolumeViewModel)volume));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occurred! ({ex.Message})");
            }
        }

        private async void AddVolumeAsync()
        {
            var newVolume = new VolumeViewModel()
            {
                BookId = SelectedBook.Id
            };

            var volumeDto = (VolumeDTO)newVolume;

            try
            {
                await _service.CreateVolumeAsync(volumeDto);
                newVolume.Id = volumeDto.Id;
                newVolume.BookId = volumeDto.BookId;
                Volumes.Add(newVolume);
                SelectedVolume = newVolume;
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occurred! ({ex.Message})");
            }
        }

        private async void DeleteVolumeAsync(VolumeViewModel volume)
        {
            try
            {
                await _service.DeleteVolumeAsync(volume.Id);
                Volumes.Remove(volume);
                SelectedVolume = null;
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occurred! ({ex.Message})");
            }
        }

        #endregion

        #region  Rents

        private async void LoadRentsAsync(VolumeViewModel volume)
        {
            if (volume is null)
            {
                Rents = null;
                return;
            }

            try
            {
                Rents = new ObservableCollection<RentViewModel>((await _service.LoadRentsAsync(volume.Id))
                    .Select(rent => (RentViewModel)rent));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occurred! ({ex.Message})");
            }
        }
        private void StartEditRent()
        {
            EditableRent = SelectedRent.ShallowClone();
            StartingRentEdit?.Invoke(this,EventArgs.Empty);
        }

        private void CancelRentEdit()
        {
            EditableRent = null;
            FinishingRentEdit?.Invoke(this, EventArgs.Empty);
        }

        public async void SaveRentEditAsync()
        {
            try
            {
                SelectedRent.CopyFrom(EditableRent);
                await _service.UpdateRentAsync((RentDTO)SelectedRent);
                FinishingRentEdit?.Invoke(this, EventArgs.Empty);
                if (SelectedVolume is not null)
                {
                    LoadRentsAsync(SelectedVolume);
                }
                    
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occurred! ({ex.Message})");
            }
        }

        #endregion
    }
}
