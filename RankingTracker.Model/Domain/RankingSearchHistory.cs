namespace RankingTracker.Model.Domain
{
    public class RankingSearchHistory
    {
        public int Id { get; set; }

        public string SearchKey { get; set; }

        public DateTimeOffset SearchDateTimeOffset { get; set; }

        public List<Ranking> Rankings { get; set; } = new List<Ranking>();
    }
}
