using TweetAPI.Core.Entities;

namespace TweetAPI.Core.Config
{
    public interface ISettingsHandler
    {
        Settings GetSettings();
        void Validate();
    }
}
