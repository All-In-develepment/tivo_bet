namespace Application.TivoGame
{
    public class TivoGameDto
    {
        public int Id { get; set; }
        public string GameId { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public string GameDate { get; set; }
        public string GameTime { get; set; }
        public string FullTimeScore { get; set; }
        public string HalfTimeScore { get; set; }
        public string League { get; set; }
    }
}