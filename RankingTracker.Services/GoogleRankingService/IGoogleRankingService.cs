using RankingTracker.Model.Domain;

namespace RankingTracker.Services.GoogleRankingService
{
    public interface IGoogleRankingService
    {
        Task<RankingSearchHistory> GetRankingSearchResult(string keyword);
    }
}
