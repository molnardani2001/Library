using Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Desktop.ViewModel
{
    public class BookViewModel : ViewModelBase
    {
        private int _id;
        private string _title;
        private string _writer;
        private int _year;
        private string _isbnNumber;
        private int _popularityNumber;
        private byte[] _smallCoverImage;
        private byte[] _normalCoverImage;

        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                    _id = value; OnPropertyChanged();
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                    _title = value; OnPropertyChanged();
            }
        }

        public string Writer
        {
            get => _writer;
            set
            {
                if(_writer != value)
                    _writer = value; OnPropertyChanged();
            }
        }

        public int Year
        {
            get => _year;
            set
            {
                if(_year != value)
                    _year = value; OnPropertyChanged();
            }
        }

        public int PopularityNumber
        {
            get => _popularityNumber;
            set
            {
                if(_popularityNumber != value)
                    _popularityNumber = value; OnPropertyChanged();
            }
        }

        public string ISBNNumber
        {
            get => _isbnNumber;
            set
            {
                if(_isbnNumber != value)
                    _isbnNumber = value; OnPropertyChanged();
            }
        }

        public byte[] SmallCoverImage
        {
            get => _smallCoverImage;
            set
            {
                if(_smallCoverImage != value)
                    _smallCoverImage = value; OnPropertyChanged();
            }
        }

        public byte[] NormalCoverImage
        {
            get => _normalCoverImage;
            set
            {
                if(_normalCoverImage != value)
                    _normalCoverImage = value; OnPropertyChanged();
            }
        }

        public BookViewModel ShallowClone()
        {
            return (BookViewModel)this.MemberwiseClone();
        }

        public void CopyFrom(BookViewModel rhs)
        {
            Id = rhs.Id;
            Title = rhs.Title;
            Writer = rhs.Writer;
            Year = rhs.Year;
            ISBNNumber = rhs.ISBNNumber;
            PopularityNumber = rhs.PopularityNumber;
            SmallCoverImage = rhs.SmallCoverImage;
            NormalCoverImage = rhs.NormalCoverImage;
        }

        public static explicit operator BookViewModel(BookDTO dto) => new BookViewModel()
        {
            Id = dto.Id,
            Title = dto.Title,
            Writer = dto.Writer,
            Year = dto.Year,
            ISBNNumber = dto.ISBNNumber,
            PopularityNumber = dto.PopularityNumber,
            SmallCoverImage = dto.SmallCoverImage,
            NormalCoverImage = dto.NormalCoverImage,
        };

        public static explicit operator BookDTO(BookViewModel vm) => new BookDTO()
        {
            Id = vm.Id,
            Title = vm.Title,
            Writer = vm.Writer,
            Year = vm.Year,
            ISBNNumber = vm.ISBNNumber,
            PopularityNumber = vm.PopularityNumber,
            SmallCoverImage = vm.SmallCoverImage,
            NormalCoverImage = vm.NormalCoverImage,
        };
    }
}
