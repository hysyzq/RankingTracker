namespace RankingTracker.Contract
{
    // contract can export to nuget allow client/web to use.
    public class RankingTrackRequest
    {
        public string SearchText { get; set; }

        public string SearchUrl { get; set; }
    }
}
