using System;
using System.Globalization;
using System.IO;
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

        public string FormatContent(Response response)
        {
            string[] fields = new string[]
            {
                response.Id,
                response.Text
            };

            return string.Join('|', fields) + System.Environment.NewLine;
        }

        public string MoveFileToDesktop()
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
