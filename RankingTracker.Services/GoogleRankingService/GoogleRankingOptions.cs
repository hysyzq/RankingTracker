namespace RankingTracker.Services.GoogleRankingService
{
    public class GoogleRankingOptions
    {
        public string Uri { get; set; }

        public string QueryFormat { get; set; }

        public bool UseChromeDriver { get; set; } = false;
    }
}
