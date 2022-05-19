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
    public class VolumesController : ControllerBase
    {
        private readonly ILibraryService _service;
        private readonly IMapper _mapper;

        public VolumesController(ILibraryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        //GET: api/Volumes
        [HttpGet]
        public ActionResult<IEnumerable<VolumeDTO>> GetVolumes(int bookId)
        {
            try
            {
                return _service
                    .Volumes
                    .Where(volume => volume.BookId == bookId)
                    .Select(volume => _mapper.Map<VolumeDTO>(volume))
                    .ToList();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<VolumeDTO> GetVolume(int id)
        {
            try
            {
                var volume = _service.GetVolume(id);
                if(volume is null)
                    return NotFound();
                return _mapper.Map<VolumeDTO>(volume);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult<VolumeDTO> PostVolume(VolumeDTO volumeDto)
        {
            var volume = _mapper.Map<Volume>(volumeDto);
            var newItem = _service.CreateVolume(volume);

            if (newItem is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            else
            {
                return CreatedAtAction(nameof(GetVolume), new { id = volume.Id },
                    _mapper.Map<VolumeDTO>(volume));
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteVolume(int id)
        {
            try
            {
                if (_service.DeleteVolume(id))
                {
                    return Ok();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status405MethodNotAllowed);
            }
            

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
