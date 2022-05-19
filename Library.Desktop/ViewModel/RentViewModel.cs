using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Data;

namespace Library.Desktop.ViewModel
{
    public class RentViewModel : ViewModelBase
    {
        private int _id;
        private int _volumeId;
        private int _visitorId;
        private DateTime _start;
        private DateTime _end;
        private bool _isActive;

        public int Id
        {
            get => _id;
            set
            {
                if(_id != value)
                    _id = value; OnPropertyChanged();
            }
        }

        public int VolumeId
        {
            get => _volumeId;
            set
            {
                if(_volumeId != value)
                    _volumeId = value; OnPropertyChanged();
            }
        }

        public int VisitorId
        {
            get => _visitorId;
            set
            {
                if(_visitorId != value)
                    _visitorId = value; OnPropertyChanged();
            }
        }

        public DateTime Start
        {
            get => _start;
            set
            {
                if(_start != value)
                    _start = value; OnPropertyChanged();
            }
        }

        public DateTime End
        {
            get => _end;
            set
            {
                if (_end != value)
                    _end = value; OnPropertyChanged();
            }
        }

        public Boolean IsActive
        {
            get => _isActive;
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    OnPropertyChanged();
                }
                    
            }
        }



        public String ActiveAsString
        {
            get => IsActive ? "Igen" : "Nem";
        }


        public RentViewModel ShallowClone()
        {
            return (RentViewModel)this.MemberwiseClone();
        }

        public void CopyFrom(RentViewModel rhs)
        {
            Id = rhs.Id;
            VolumeId = rhs.VolumeId;
            VisitorId = rhs.VisitorId;
            Start = rhs.Start;
            End = rhs.End;
            IsActive = rhs.IsActive;
        }

        public static explicit operator RentViewModel(RentDTO dto) => new RentViewModel()
        {
            Id = dto.Id,
            VolumeId = dto.VolumeId,
            VisitorId = dto.VisitorId,
            Start = dto.Start,
            End = dto.End,
            IsActive = dto.IsActive
        };

        public static explicit operator RentDTO(RentViewModel vm) => new RentDTO()
        {
            Id = vm.Id,
            VolumeId = vm.VolumeId,
            VisitorId = vm.VisitorId,
            Start = vm.Start,
            End = vm.End,
            IsActive = vm.IsActive
        };
    }
}
