using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using AutoMapper;
using Castle.Core.Logging;
using Xunit;
using Library.Data;
using Library.WebApi.Controllers;
using Library.WebApi.Mappings;
using Library.WebApi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Library.Tests
{
    public class ControllersTest : IDisposable
    {
        private readonly LibraryContext _context;
        private readonly LibraryService _service;
        private readonly BooksController _booksController;
        private readonly VolumesController _volumesController;
        private readonly RentsController _rentsController;
        private readonly AccountController _accountController;
        private readonly IMapper _mapper;

        public ControllersTest()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _context = new LibraryContext(options);
            TestDbInitializer.Initialize(_context,"..\\..\\..\\App_Data");

            _context.ChangeTracker.Clear();
            _service = new LibraryService(_context);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BookProfile());
                cfg.AddProfile(new BookDtoProfile());
                cfg.AddProfile(new VolumeProfile());
                cfg.AddProfile(new VolumeDtoProfile());
                cfg.AddProfile(new RentProfile());
                cfg.AddProfile(new RentDtoVolume());
            });

            _mapper = new Mapper(config);
            _booksController = new BooksController(_service, _mapper);
            _volumesController = new VolumesController(_service, _mapper);
            _rentsController = new RentsController(_service, _mapper);

        }


        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        #region Books

        [Fact]
        public void GetBooksTest()
        {
            var result = _booksController.GetBooks();

            var content = Assert.IsAssignableFrom<IEnumerable<BookDTO>>(result.Value);
            Assert.Equal(45, content.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(35)]
        public void GetBookByIdTest(Int32 id)
        {
            var result = _booksController.GetBook(id);

            var content = Assert.IsAssignableFrom<BookDTO>(result.Value);

            Assert.Equal(id,content.Id);
        }

        [Fact]
        public void GetInvalidBookTest()
        {
            int id = 46;

            var result = _booksController.GetBook(id);

            Assert.IsAssignableFrom<NotFoundResult>(result.Result);
        }

        [Fact]
        public void PostBookTest()
        {
            var newBook = new BookDTO
            {
                Title = "Harry Potter �s a F�lv�r Herceg",
                Writer = "J. K. Rowling",
                ISBNNumber = "9789633247365",
                Year = 2020,
                SmallCoverImage = File.ReadAllBytes("..\\..\\..\\App_Data\\tests\\harry_potter_es_a_felver_herceg_thumb.jpg"),
                NormalCoverImage = File.ReadAllBytes("..\\..\\..\\App_Data\\tests\\harry_potter_es_a_felver_herceg.jpg")
            };

            var count = _context.Books.Count();

            var result = _booksController.PostBook(newBook);

            var objectResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result.Result);
            var content = Assert.IsAssignableFrom<BookDTO>(objectResult.Value);
            Assert.Equal(count + 1, _context.Books.Count());
        }

        [Fact]
        public void PostAlreadyExistsBookTest()
        {
            var newBook = new BookDTO
            {
                Title = "A f�redi l�ny",
                Writer = "Kar�dy Anna",
                Year = 2022,
                ISBNNumber = "9789635700295",
                PopularityNumber = 0,
                SmallCoverImage = File.ReadAllBytes("..\\..\\..\\App_Data\\a_furedi_lany_thumb.jpg"),
                NormalCoverImage = File.ReadAllBytes("..\\..\\..\\App_Data\\a_furedi_lany.jpg")
            };

            var count = _context.Books.Count(); //lek�rj�k a k�nyvek sz�m�t

            var result = _booksController.PostBook(newBook); //megpr�b�ljuk hozz�adni azt a k�nyvet aminek az adataival (c�m, �r�, �v, isbn sz�m) m�r l�tezik k�nyv

            var statusCodeResult = result.Result as StatusCodeResult; //hib�t kapunk

            Assert.Equal(409,statusCodeResult.StatusCode); // conflict hib�val t�rt�nk vissza

            Assert.Equal(count, _context.Books.Count()); //�gy nem t�rt�nt v�ltoz�s
        }

        #endregion

        #region Volumes

        [Theory]
        [InlineData(1)]
        [InlineData(20)]
        [InlineData(40)]
        public void GetVolumesTest(Int32 bookId)
        {

            var result = _volumesController.GetVolumes(bookId);

            var content = Assert.IsAssignableFrom<IEnumerable<VolumeDTO>>(result.Value);

            Assert.Equal(5, content.Count());
        }
        [Theory]
        [InlineData(5)]
        [InlineData(60)]
        [InlineData(99)]
        public void GetVolumeTest(int volumeId)
        {
            var result = _volumesController.GetVolume(volumeId);

            var content = Assert.IsAssignableFrom<VolumeDTO>(result.Value);
            Assert.Equal(volumeId, content.Id);
        }

        [Fact]
        public void GetInvalidVolumeTest()
        {
            var id = 1000;

            var result = _volumesController.GetVolume(id);

            Assert.IsAssignableFrom<NotFoundResult>(result.Result);
        }

        [Fact]
        public void PostVolumeTest()
        {
            var newVolume = new VolumeDTO
            {
                BookId = 1
            };

            var count = _service.GetBook(1).Volumes.Count;

            var result = _volumesController.PostVolume(newVolume);

            var objectResult = Assert.IsAssignableFrom<CreatedAtActionResult>(result.Result);
            var content = Assert.IsAssignableFrom<VolumeDTO>(objectResult.Value);
            Assert.Equal(count + 1, _service.GetBook(1).Volumes.Count);
        }

        [Fact]
        public void DeleteVolumeTest()
        {
            var count = _service.GetBook(1).Volumes.Count;

            var result = _volumesController.DeleteVolume(1);

            Assert.Equal(count-1, _service.GetBook(1).Volumes.Count);
        }

        [Fact]
        public void DeleteVolumeWithActiveRentTest()
        {
            //�sszes foglal�sok sz�ma
            var rentsCount = _context.Rents.Count();

            
            var rent =_mapper.Map<RentDTO>(_service.GetRentById(1));

            rent.IsActive = true; // lek�rj�k az 1-es azonos�t�j� foglal�st �s �t�ll�tjuk akt�vra

            var updateRentResult = _rentsController.PutRent(1, rent);

            Assert.IsAssignableFrom<OkResult>(updateRentResult); // siker�lt �t�ll�tani

            var volumesCount = _service.GetBook(1).Volumes.Count; // lek�rj�k az els� k�nyvh�z tartoz� k�tetek sz�m�t

            var deleteVolumeResult = _volumesController.DeleteVolume(1); // megpr�b�ljuk kit�r�lni az els� k�tetet, amihez tartozik akt�v k�lcs�nz�s

            var deleteVolumeStatusCodeResult = deleteVolumeResult as StatusCodeResult;

            Assert.Equal(405,deleteVolumeStatusCodeResult.StatusCode); // Method not allowed status code-al t�r�nk vissza

            Assert.Equal(volumesCount, _service.GetBook(1).Volumes.Count); // a k�tetek sz�ma ezzel nem v�ltozik

            rent.IsActive = false;

            updateRentResult = _rentsController.PutRent(1, rent); //inakt�vv� tessz�k a k�lcs�nz�st

            Assert.IsAssignableFrom<OkResult>(updateRentResult); // siker�lt �t�ll�tani

            deleteVolumeResult = _volumesController.DeleteVolume(1); //megpr�b�ljuk kit�r�lni a k�tetet


            Assert.Equal(volumesCount - 1, _service.GetBook(1).Volumes.Count); //sikeresen kit�rl�dik a k�tet, mert nem tartozik hozz� akt�v k�lcs�nz�s

            Assert.Equal(rentsCount - 2, _context.Rents.Count()); // a k�tethez tartoz� k�lcs�nz�sek is t�rl�dnek az adatb�zisb�l

        }

        #endregion

        #region Rents

        [Fact]
        public void GetRentsTest()
        {
            var result = _rentsController.GetRents(1);

            var content = Assert.IsAssignableFrom<IEnumerable<RentDTO>>(result.Value);
            Assert.Equal(2, content.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetRentByIdTest(Int32 rentId)
        {
            var result = _rentsController.GetRent(rentId);

            var content = Assert.IsAssignableFrom<RentDTO>(result.Value);
            Assert.Equal(rentId,content.Id);
        }

        [Fact]
        public void GetInvalidRentTest()
        {
            var id = 4;

            var result = _rentsController.GetRent(id);

            Assert.IsAssignableFrom<NotFoundResult>(result.Result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void PutRentTest(int rentId)
        {
            bool newIsActive = true;
            var rentDto = _mapper.Map<RentDTO>(_service.GetRentById(rentId));

            rentDto.IsActive = newIsActive;

            

            var result = _rentsController.PutRent(rentId, rentDto);


            var requestedResult = Assert.IsAssignableFrom<OkResult>(result);
            var updatedRent = _rentsController.GetRent(rentId);
            Assert.Equal(updatedRent?.Value?.IsActive, newIsActive);
        }

        [Fact]
        public void UpdateRentWhenAnotherRentIsActiveTest()
        {
            var rentDtoOne = _mapper.Map<RentDTO>(_service.GetRentById(1)); //lek�rj�k az els� k�lcs�nz�st az els� k�tethez

            rentDtoOne.IsActive = true; //�t�ll�tjuk akt�vra

            var response1 = _rentsController.PutRent(1, rentDtoOne);

            Assert.IsAssignableFrom<OkResult>(response1); //siker�lt

            var rentDtoTwo = _mapper.Map<RentDTO>(_service.GetRentById(2)); //lek�rj�k a n�sodik k�ls�nz�st az els� k�tethez

            rentDtoTwo.IsActive = true; //�t�ll�tjuk akt�vra

            var response2 = _rentsController.PutRent(2, rentDtoTwo);

            var response2StatusCode = response2 as StatusCodeResult;

            Assert.Equal(405,response2StatusCode.StatusCode);//method not allowed st�tuszk�ddal t�r�nk vissza, mert a k�tethez m�r tartozik egy akt�v k�lcs�nz�s

            rentDtoOne.IsActive = false; //vissza�ll�tjuk az els� k�lcs�nz�st inakt�vv�

            response1 = _rentsController.PutRent(1, rentDtoOne);

            Assert.IsAssignableFrom<OkResult>(response1); //siker�lt

            rentDtoTwo.IsActive = true; //�jra megpr�b�ljuk �t�ll�tani a foglal�st akt�vv�

            response2 = _rentsController.PutRent(2, rentDtoTwo);

            Assert.IsAssignableFrom<OkResult>(response2); //siker�lt, mert m�r nem tartozik hozz� m�sik akt�v k�lcs�nz�s
        }

        [Fact]
        public void DoesntDisplayExpiredRentsTest()
        {
            var count = _service.GetRentsToVolume(1);

            _context.Rents.Add(new Rent
            {
                VolumeId = 1,
                Start = DateTime.Today - TimeSpan.FromDays(10),
                End = DateTime.Today - TimeSpan.FromDays(3),
                IsActive = false,
                VisitorId = 1
            });

            _context.SaveChanges();

            //att�l f�ggetlen�l, hogy hozz�adtuk m�ltbeli �gy nem list�z�dik ki
            Assert.Equal(count, _service.GetRentsToVolume(1));
        }

        #endregion

        
    }
}