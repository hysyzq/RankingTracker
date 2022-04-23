using RankingTracker.Model.Domain;
using System.Text.RegularExpressions;

namespace RankingTracker.Services.GoogleRankingService
{
    public static class GoogleContentMapper
    {

        private static Regex linkParser = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static Regex contentParser = new Regex(@"/url\?q=\b(?:https?://|www\.)[a-zA-Z0-9._-]+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static string ExtractUrlLower(this string content)
        {
            List<string> urls = new List<string>();
            foreach (Match m in linkParser.Matches(content))
            {
                urls.Add(m.Value);
            }
            return urls[0].ToLower() ?? "";
        }


        public static RankingSearchHistory ToRankingSearchHistory(this string content, string keyword)
        {
            RankingSearchHistory result = new RankingSearchHistory
            {
                SearchDateTimeOffset = DateTimeOffset.Now,
                SearchKey = keyword
            };
            int rank = 0;
            foreach (Match m in contentParser.Matches(content))
            {
                result.Rankings.Add(new Ranking
                {
                    Key = m.Value.Substring(1),
                    Rank = ++rank
                });
            }

            return result;
        }

    }
}
