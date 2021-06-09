using System;

namespace TweetAPI.Core.Entities
{
    public class Response
    {
        public string CreatedAt { get; set; } = "";
        public string Id { get; set; } = "";
        public string Text { get; set; } = "";

        public bool IsCreated()
        {
            return CreatedAt.Length > 0;
        }
    }
}
