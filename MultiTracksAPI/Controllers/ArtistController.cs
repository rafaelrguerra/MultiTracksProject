using Microsoft.AspNetCore.Mvc;
using MultiTracksAPI.Models;
using MultiTracksAPI.Repositories;

namespace MultiTracksAPI.Controllers
{
    [ApiController]
    [Route("api.multitracks.com/[controller]")]
    public class ArtistController : ControllerBase
    {
        private IArtistRepository _artistRepository;
        public ArtistController(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }
        [HttpGet("search/name")]
        public IActionResult Get(string name)
        {
            var result = _artistRepository.GetArtistByName(name);
            return result != null ? Ok(result) : NotFound("The artist was not found.");
        }

        [HttpPost("add")]
        public IActionResult Post(ArtistRequest request)
        {
            if (string.IsNullOrEmpty(request.Title))
                return BadRequest("Invalid information.");

            var isAdded = _artistRepository.AddArtist(request);
            return isAdded ? Ok(request.Title + " was successfully added.") : BadRequest("There was an error adding the artist.");
        }
    }
}