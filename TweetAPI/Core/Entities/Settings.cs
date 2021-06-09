namespace TweetAPI.Core.Entities
{
    public class Settings
    {
        public string Url { get; set; } = "";
        public string ApiKey { get; set; } = "";
        public string ApiSecretKey { get; set; } = "";
        public string AccessToken { get; set; } = "";
        public string AccessTokenSecret { get; set; } = "";
        public string BearerToken { get; set; } = "";
    }
}
