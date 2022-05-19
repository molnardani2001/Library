using AutoMapper;
using Library.Data;

namespace Library.WebApi.Mappings
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDTO>();
        }
    }

    public class VolumeProfile : Profile
    {
        public VolumeProfile()
        {
            CreateMap<Volume, VolumeDTO>();
        }
    }

    public class RentProfile : Profile
    {
        public RentProfile()
        {
            CreateMap<Rent, RentDTO>();
        }
    }

    public class BookDtoProfile : Profile
    {
        public BookDtoProfile()
        {
            CreateMap<BookDTO, Book>();
        }
    }

    public class VolumeDtoProfile : Profile
    {
        public VolumeDtoProfile()
        {
            CreateMap<VolumeDTO, Volume>();
        }
    }

    public class RentDtoVolume : Profile
    {
        public RentDtoVolume()
        {
            CreateMap<RentDTO, Rent>();
        }
    }
}
