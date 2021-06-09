using RestSharp;
using RestSharp.Serialization;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TweetAPI.Core.Entities;
using TweetAPI.Core.Repos;
using TweetAPI.Infra.Clients.Parsers;
using TweetAPI.Infra.Config;

namespace TweetAPI.Infra.Clients
{
    public class TwitterClient : ITwitterRepo
    {
        private readonly IRestClient _client;
        private readonly ITwitterParser _parser;

        public TwitterClient(
            IRestClient client,
            ITwitterParser parser)
        {
            _client = client;
            _parser = parser;   
        }

        public async Task<List<Response>> SearchTweets(string query)
        {
            var request = new RestRequest($"2/tweets/search/recent", Method.GET);
            request.AddHeader("Content-type", ContentType.Json);
            request.AddParameter("query", query);
            var response = await _client.ExecuteAsync(request, CancellationToken.None);

            ResponseHandler.HandleResponse(response);

            return _parser.ParseList(response.Content);
        }

        public async Task<Response> Post(string tweet)
        {
            var request = new RestRequest($"1.1/statuses/update.json?status={tweet}", Method.POST);
            request.AddHeader("Content-type", ContentType.Json);
            var response = await _client.ExecuteAsync(request, CancellationToken.None);
            return _parser.Parse(response.Content);
        }
    }
}
