namespace RankingTracker.Model.Domain
{
    // can have repository/DB to store the records for reporting purpose.
    public class RankingSearchHistory
    {
        public int Id { get; set; }

        public string SearchKey { get; set; }

        public DateTimeOffset SearchDateTimeOffset { get; set; }

        public List<Ranking> Rankings { get; set; } = new List<Ranking>();
    }
}
