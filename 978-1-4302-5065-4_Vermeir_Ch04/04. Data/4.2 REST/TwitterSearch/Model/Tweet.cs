using Newtonsoft.Json;

namespace TwitterSearch.Model
{
    public class Tweet
    {
        [JsonProperty(PropertyName = "from_user_name")]
        public string User { get; set; }

        [JsonProperty(PropertyName = "text")]        
        public string Message { get; set; }
    }
}
