using System.Collections.Generic;
using System.Threading.Tasks;
using TweetAPI.Core.Entities;

namespace TweetAPI.Core.Repos
{
    public interface ITwitterRepo
    {
        //Task<Tweet> GetLastTweet();
        Task<List<Response>> SearchTweets(string query);
    }
}
