using Moq;
using NUnit.Framework;
using TweetAPI.Core.Repos;

namespace TweetAPI.Test.Infra.Clients
{
    public class TwitterClientTests
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var _client = new Mock<ITwitterRepo>();

        }
    }
}
