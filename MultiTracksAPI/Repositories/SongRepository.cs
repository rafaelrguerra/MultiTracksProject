using Dapper;
using MultiTracksAPI.Models;
using System.Data.SqlClient;

namespace MultiTracksAPI.Repositories
{
    public class SongRepository : ISongRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;

        public SongRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<SongResponse> GetSongs(int pageSize, int pageNumber)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Calculate the offset based on the page number and page size
                int offset = (pageNumber - 1) * pageSize;

                // Query to fetch the paged data
                string query = $"SELECT song.SongId, song.Title, song.DateCreation, album.Title AS Album, artist.title AS Artist, " +
                                    $"song.BPM, song.TimeSignature, song.Multitracks, song.CustomMix, song.Chart, " +
                                    $"song.RehearsalMix, song.Patches, song.SongSpecificPatches, song.ProPresenter " +
                                $"FROM Song song " +
                                $"JOIN Album album ON album.albumID = song.albumID " +
                                $"JOIN Artist artist ON artist.artistID = song.artistID " +
                                $"ORDER BY Songid OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY";

                // Use Dapper to execute the query and return the results
                var results = connection.Query<SongResponse>(query);
                return results;
            }

        }
    }
}
