using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TweetAPI.Core.Entities;

namespace TweetAPI.Infra.Clients.Parsers
{
    public class TwitterParser : ITwitterParser
    {
        public Response Parse(string jsonString)
        {
            dynamic obj = JsonConvert.DeserializeObject(jsonString);

            var tweet = new Response()
            {
                CreatedAt = obj.created_at
            };

            return tweet;
        }

        public List<Response> ParseList(string jsonString)
        {
            dynamic obj = JsonConvert.DeserializeObject(jsonString);

            var data = (IEnumerable<dynamic>)obj.data;

            return data.Select(item =>
            {
                var response = new Response()
                {
                    Id = item?.id,
                    Text = item?.text
                };
                return response;
            }).ToList();
        }
    }
}
