using RankingTracker.Model.Domain;

namespace RankingTracker.Services.GoogleRankingService
{
    public static class GoogleContentMapper
    {
        public static RankingSearchHistory ToRankingSearchHistory(this string content, string key)
        {
            return new RankingSearchHistory
            {
                Id = 1,
                SearchDateTimeOffset = DateTimeOffset.Now,
                SearchKey = key,
                Rankings = new List<Ranking> { 
                    new Ranking{Id = 1, Rank = 1, Key = "www.smokeball.com.au" },
                    new Ranking{Id = 2, Rank = 2, Key = "www.apple.com" },
                    new Ranking{Id = 3, Rank = 3, Key = "www.smokeball.com.au" },
                    new Ranking{Id = 4, Rank = 4, Key = "www.facebook.com" },
                }
            };
        }
    }
}
