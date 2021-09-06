using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using ShellProgressBar;
using TweetAPI.Core.Entities;

namespace TweetAPI.Infra.IO
{
    public class FileHandler : IDisposable
    {
        private string _filePath;
        private readonly TextWriter _writer;
        public TextWriter Writer => _writer;

        public FileHandler()
        {
            _filePath = $"TweetAPI-{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss", CultureInfo.InvariantCulture)}.txt";
            _writer = File.CreateText(_filePath);
        }

        public async Task<string> HandleReponse(
            List<Response> response,
            TextWriter writer,
            IProgress<double> progress)
        {
            foreach (var tweet in response)
            {
                var content = FormatContent(tweet);
                await writer.WriteAsync(content);
                await writer.FlushAsync();
                progress.Report(1);
            }

            return MoveFileToDesktop();
        }

        private string FormatContent(Response response)
        {
            var fields = new string[]
            {
                response.Id,
                response.Text.Replace("\n", "").Replace("\r", "")
            };

            return string.Join('|', fields) + System.Environment.NewLine;
        }

        private string MoveFileToDesktop()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fullPath = Path.Combine(desktopPath, _filePath);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            _writer.Close();
            File.Move(_filePath, fullPath);

            return fullPath;
        }

        public void Dispose()
        {
            _writer.Close();
            _writer.Dispose();
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }
    }
}
