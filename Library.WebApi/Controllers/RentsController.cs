using System.Data;
using AutoMapper;
using Library.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Library.WebApi.Models;

namespace Library.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentsController : ControllerBase
    {
        private readonly ILibraryService _service;
        private readonly IMapper _mapper;

        public RentsController(ILibraryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RentDTO>> GetRents(int volumeId)
        {
            try
            {
                return _service
                    .GetRentsToVolume(volumeId)
                    .Select(rent => _mapper.Map<RentDTO>(rent))
                    .ToList();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<RentDTO> GetRent(int id)
        {
            try
            {
                var rent = _service.GetRentById(id);
                if (rent is null) return NotFound();
                return _mapper.Map<RentDTO>(rent);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult PutRent(int id, RentDTO rentDto)
        {
            if (id != rentDto.Id)
            {
                return BadRequest();
            }

            var rent = _mapper.Map<Rent>(rentDto);
            try
            {
                if (_service.UpdateRent(rent))
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            catch(DataException)
            {
                return StatusCode(StatusCodes.Status405MethodNotAllowed);
            }
            
        }
    }
}
