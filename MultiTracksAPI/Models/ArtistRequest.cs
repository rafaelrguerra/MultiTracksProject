namespace MultiTracksAPI.Models
{
    public class ArtistRequest
    {
        public ArtistRequest()
        {
            DateCreation = DateTime.Now;
        }
        public DateTime DateCreation { get; }
        public string Title { get; set; }
        public string Biography { get; set; }
        public string ImageURL { get; set; }
        public string HeroURL { get; set; }
    }
}
