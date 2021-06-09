using Autofac;
using System;
using System.Threading;
using System.Threading.Tasks;
using TweetAPI.Core.Repos;
using TweetAPI.Infra.Autofac;

namespace TweetAPI.Infra.UI
{
    internal static class ConsoleApp
    {
        static async Task Main()
        {
            Console.WriteLine("type your query: ");
            var query = Console.ReadLine();

            if (string.IsNullOrEmpty(query))
                throw new Exception("Invalid query");

            var container = AutofacContainerBuilder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var twitterClient = scope.Resolve<ITwitterRepo>();

                var responseSearch = await twitterClient.SearchTweets(query);
                foreach (var item in responseSearch)
                {
                    Console.WriteLine(item.Text);
                }
            }
        }
    }
}
