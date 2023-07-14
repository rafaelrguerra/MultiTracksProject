using Dapper;
using MultiTracksAPI.Models;
using System.Data.SqlClient;

namespace MultiTracksAPI.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;
        public ArtistRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public ArtistResponse GetArtistByName(string name)
        {
            string sql = @"SELECT * FROM Artist where Title = @Name";
            using var connection = new SqlConnection(connectionString);
            return connection.QueryFirstOrDefault<ArtistResponse>(sql, new { Name = name });

        }

        public bool AddArtist(ArtistRequest request)
        {
            string sql = @"INSERT INTO [dbo].[Artist]
                               (dateCreation
                               ,title
                               ,biography
                               ,imageURL
                               ,heroURL)
                         VALUES
                               (@DateCreation
                               ,@Title
                               ,@Biography
                               ,@ImageURL
                               ,@HeroURL)";

            using var connection = new SqlConnection(connectionString);
            return connection.Execute(sql, request) > 0;
        }
    }
}