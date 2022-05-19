using AutoMapper;
using Library.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Library.Data;
using Microsoft.AspNetCore.Authorization;

namespace Library.WebApi.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    //[ApiConventionType(typeof(DefaultApiConventions))]
    public class BooksController : ControllerBase
    {
        private readonly ILibraryService _service;
        private readonly IMapper _mapper;

        public BooksController(ILibraryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Könyvek lekérdezése
        /// </summary>
        // GET: api/Books
        [HttpGet]
        public ActionResult<IEnumerable<BookDTO>> GetBooks()
        {
            try
            {
                return _service
                    .Books
                    .Select(book => _mapper.Map<BookDTO>(book))
                    .ToList();
            }
            catch
            {
                // Internal Server Error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //GET: api/Books/1
        [HttpGet("{id}")]
        public ActionResult<BookDTO> GetBook(int id)
        {
            var book = _service.GetBook(id);
            if (book is null)
                return NotFound();

            return _mapper.Map<BookDTO>(book);
        }

        [Authorize]
        [HttpPost]
        public ActionResult<BookDTO> PostBook([FromBody] BookDTO bookDto)
        {

            try
            {
                var book = _service.CreateBook(_mapper.Map<Book>(bookDto));
                if (book is null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                else
                {
                    return CreatedAtAction(nameof(GetBook), new { id = book.Id },
                        _mapper.Map<BookDTO>(book));
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status409Conflict);
            }

            
        }
    }
}
