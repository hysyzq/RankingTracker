using Contract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RankingTracker.Connector
{
    internal class ApiConnector 
    {
        public string TrackRanking (string searchText, string searchUrl)
        {
            RankingTrackRequest request = new RankingTrackRequest
            {
                SearchText = searchText,
                SearchUrl = searchUrl
            };
            RankingTrackResponse rankingTrackResponse = new RankingTrackResponse();
            try
            {
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(uriString: ConfigurationManager.AppSettings.Get("ApiBaseAddress").ToString());

                    var apiResponse = client.PostAsJsonAsync(ConfigurationManager.AppSettings.Get("ApiRoute").ToString(), request).Result;

                    if (apiResponse.IsSuccessStatusCode)
                    {
                        var content = apiResponse.Content.ReadAsStringAsync().Result;
                        rankingTrackResponse = JsonConvert.DeserializeObject<RankingTrackResponse>(content);
                        
                        // mapping
                        return rankingTrackResponse.ToString();
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }


            return "1,2,3,4";
        }
    }
}
