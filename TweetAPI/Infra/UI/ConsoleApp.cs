using Autofac;
using System;
using System.Threading.Tasks;
using TweetAPI.Core.Repos;
using TweetAPI.Infra.Autofac;

namespace TweetAPI.Infra.UI
{
    internal static class ConsoleApp
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("type your query: ");
            var query = Console.ReadLine();

            if (string.IsNullOrEmpty(query))
                throw new Exception("Invalid tweet");

            var container = AutofacContainerBuilder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var twitterClient = scope.Resolve<ITwitterRepo>();
                //var response = await twitterClient.Post(tweet);
                //Console.WriteLine("success: " + response.IsCreated());
                //Thread.Sleep(5000);
                var response = await twitterClient.SearchTweets(query);
                foreach (var item in response)
                {
                    Console.WriteLine(item.Text);
                }
            }
        }
    }
}
