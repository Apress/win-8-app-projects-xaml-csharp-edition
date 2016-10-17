using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TwitterSearch.Model;
using Windows.UI.Popups;

namespace TwitterSearch
{
    public class TwitterService
    {
        public async Task<List<Tweet>> GetTweets(string query)
        {
            var url = String.Format("http://search.twitter.com/search.json?q={0}", query);

            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                MessageDialog dialog =
                    new MessageDialog("No active internetconnection found, please connect to the internet and try again");
                dialog.ShowAsync();

                return null;
            }
            else
            {
                HttpClient client = new HttpClient();

                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                JObject obj = JObject.Parse(json);
                var jsonResults = obj["results"];

                return await JsonConvert.DeserializeObjectAsync<List<Tweet>>(jsonResults.ToString());
            }

        }
    }
}
