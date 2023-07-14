using MultiTracksAPI.Models;

namespace MultiTracksAPI.Repositories
{
    public interface IArtistRepository
    {
        ArtistResponse GetArtistByName(string name);
        bool AddArtist(ArtistRequest request);
    }
}