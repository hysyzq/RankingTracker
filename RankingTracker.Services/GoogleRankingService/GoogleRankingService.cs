using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RankingTracker.Model.Domain;
using System.Net;

namespace RankingTracker.Services.GoogleRankingService
{
    public class GoogleRankingService : IGoogleRankingService
    {
        private readonly GoogleRankingOptions _googleOptions;
        private readonly ILogger<GoogleRankingService> _logger;

        public GoogleRankingService(IOptionsMonitor<GoogleRankingOptions> googleOptions, ILogger<GoogleRankingService> logger)
        {
            _googleOptions = googleOptions.CurrentValue;
            _logger = logger;
        }

        public async Task<RankingSearchHistory> GetRankingSearchResult(string keyword)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
                    client.DefaultRequestHeaders.Accept.Clear();
                    var uriStr = string.Format(_googleOptions.QueryFormat, _googleOptions.Uri, keyword);
                    var response = await client.GetStringAsync(uriStr);
                    var result = response.ToRankingSearchHistory(keyword);
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
