using System;
using System.Collections.Generic;
using System.Text;
using TweetAPI.Core.Entities;

namespace TweetAPI.Infra.Clients.Parsers
{
    public interface ITwitterParser
    {
        Response Parse(string json);
        List<Response> ParseList(string json);
    }
}
