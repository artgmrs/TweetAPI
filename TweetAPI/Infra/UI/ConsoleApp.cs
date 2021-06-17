using Autofac;
using ShellProgressBar;
using System;
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
            Console.WriteLine("Type your query: ");
            var query = Console.ReadLine();
            Console.WriteLine();

            if (string.IsNullOrEmpty(query))
                throw new Exception("Invalid query");

            var container = AutofacContainerBuilder.Build();

            var options = new ProgressBarOptions()
            {
                ProgressCharacter = '─',
                ProgressBarOnBottom = true
            };

            using (var progressBar = new ProgressBar(10, "Starting request...", options))
            using (var scope = container.BeginLifetimeScope())
            {
                var progress = progressBar.AsProgress<int>();
                var twitterClient = scope.Resolve<ITwitterRepo>();
                var response = await twitterClient.SearchTweets(query);

                var fileHandler = new FileHandler();
                var writer = fileHandler.Writer;

                foreach (var tweet in response)
                {
                    var content = fileHandler.FormatContent(tweet);
                    await writer.WriteAsync(content);
                    await writer.FlushAsync();
                    progress.Report(2);
                }

                var deskPath = fileHandler.MoveFileToDesktop();
                progressBar.Message = "Finished with success!";
                progressBar.Dispose();

                Console.WriteLine($"The file has been generated at {deskPath}");
                Console.WriteLine();

                Console.WriteLine("Press any key to exit the program :)");
                Console.ReadKey();
            }
        }
    }
}
