using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TweetAPI.Core.Config;
using TweetAPI.Core.Entities;

namespace TweetAPI.Infra.Config
{
    public class SettingsHandler : ISettingsHandler
    {
        private readonly string _file;
        private Settings _settings = null;

        public void Validate()
        {
            if (_settings == null)
            {
                throw new Exception($"Could not read settings from configuration file {_file}.");
            }

            foreach (var prop in _settings.GetType().GetProperties())
            {
                var tp = prop.PropertyType;
                var val = prop.GetValue(_settings, null);
                bool stringInvalid = (tp == typeof(string) && string.IsNullOrEmpty(val?.ToString()));
                bool longInvalid = (tp == typeof(long) && (val == null || (long)val == 0));
                if (stringInvalid || longInvalid)
                {
                    throw new Exception($"Field {prop.Name} is empty or invalid at {_file}");
                }
            }
        }

        public SettingsHandler(string appSettingsFile = "appsettings.json")
        {
            _file = appSettingsFile;
        }

        public Settings GetSettings()
        {
            if (_settings == null)
            {
                string jsonString = File.ReadAllText(_file);
                _settings = JsonConvert.DeserializeObject<Settings>(jsonString);
                Validate();
            }

            return _settings;
        }
    }
}
