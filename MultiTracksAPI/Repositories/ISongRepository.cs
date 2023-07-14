using MultiTracksAPI.Models;

namespace MultiTracksAPI.Repositories
{
    public interface ISongRepository
    {
        IEnumerable<SongResponse> GetSongs(int pageSize, int pageNumber);
    }
}