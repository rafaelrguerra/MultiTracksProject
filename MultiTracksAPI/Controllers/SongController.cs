using Microsoft.AspNetCore.Mvc;
using MultiTracksAPI.Repositories;

namespace MultiTracksAPI.Controllers
{
    [ApiController]
    [Route("api.multitracks.com/[controller]")]
    public class SongController : ControllerBase
    {
        private ISongRepository _songRepository;
        public SongController(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }
        [HttpGet("list/pageSize/pageNumber")]
        public IActionResult ListSongs(int pageSize = 10, int pageNumber = 1)
        {
            var songs = _songRepository.GetSongs(pageSize, pageNumber);
            return Ok(songs);
        }
    }
}
