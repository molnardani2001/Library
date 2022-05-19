using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Data;

namespace Library.Desktop.ViewModel
{
    public class VolumeViewModel : ViewModelBase
    {
        private int _id;
        private int _bookId;

        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                    _id = value; OnPropertyChanged();
            }
        }

        public int BookId
        {
            get => _bookId;
            set
            {
                if(_bookId != value)
                    _bookId = value; OnPropertyChanged();
            }
        }

        public VolumeViewModel ShallowClone()
        {
            return (VolumeViewModel)this.MemberwiseClone();
        }

        public void CopyFrom(VolumeViewModel rhs)
        {
            Id = rhs.Id;
            BookId = rhs.BookId;
        }

        public static explicit operator VolumeViewModel(VolumeDTO dto) => new VolumeViewModel()
        {
            Id = dto.Id,
            BookId = dto.BookId
        };

        public static explicit operator VolumeDTO(VolumeViewModel vm) => new VolumeDTO()
        {
            Id = vm.Id,
            BookId = vm.BookId
        };
    }
}
