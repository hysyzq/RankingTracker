using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
            try
            {
                if (_googleOptions.UseChromeDriver)
                {
                    return await GetViaChromeDriver(keyword);
                }
                return await GetViaNative(keyword);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<RankingSearchHistory> GetViaNative(string keyword)
        {
            var uriStr = string.Format(_googleOptions.QueryFormat, _googleOptions.Uri, keyword);
            using (var client = new HttpClient())
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
                client.DefaultRequestHeaders.Accept.Clear();
                var stringContent = await client.GetStringAsync(uriStr);
                return stringContent.ToRankingSearchHistory(keyword);
            }
        }

        private async Task<RankingSearchHistory> GetViaChromeDriver(string keyword)
        {
            var uriStr = string.Format(_googleOptions.QueryFormat, _googleOptions.Uri, keyword);
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            RankingSearchHistory rankingSearchHistory = new RankingSearchHistory
            {
                Id = 0,
                SearchDateTimeOffset = DateTimeOffset.Now,
                SearchKey = keyword,
            };

            using (IWebDriver selenium = new ChromeDriver(Environment.CurrentDirectory, chromeOptions))
            {
                selenium.Navigate().GoToUrl(uriStr);
                while (string.IsNullOrEmpty(selenium.Title))
                {
                    Task.Delay(100).GetAwaiter().GetResult();
                }
                var searchResults = selenium.FindElements(By.ClassName("g"));
                int rank = 0;
                for (int i = 0; i < searchResults.Count; i++)
                {
                    var item = searchResults[i].Text;
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        rank++;
                        rankingSearchHistory.Rankings.Add(new Ranking
                        {
                            Id = rank,
                            Rank = rank,
                            Key = item.ExtractUrlLower()
                        });
                    }
                }
            }
            return rankingSearchHistory;
        }

        
    }
}
