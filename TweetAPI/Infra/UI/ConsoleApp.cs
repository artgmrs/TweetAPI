using Autofac;
using System;
using System.Threading;
using System.Threading.Tasks;
using TweetAPI.Core.Repos;
using TweetAPI.Infra.Autofac;
using TweetAPI.Infra.IO;

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
                var response = await twitterClient.SearchTweets(query);

                var fileHandler = new FileHandler();
                var writer = fileHandler.Writer;

                foreach (var tweet in response)
                {
                    var content = fileHandler.FormatContent(tweet);
                    await writer.WriteAsync(content);
                    await writer.FlushAsync();                    
                }
                fileHandler.MoveFileToDesktop();
            }
        }
    }
}
