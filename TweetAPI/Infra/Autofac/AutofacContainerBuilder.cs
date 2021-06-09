using Autofac;
using RestSharp;
using RestSharp.Authenticators;
using TweetAPI.Core.Config;
using TweetAPI.Core.Repos;
using TweetAPI.Infra.Clients;
using TweetAPI.Infra.Clients.Parsers;
using TweetAPI.Infra.Config;

namespace TweetAPI.Infra.Autofac
{
    public static class AutofacContainerBuilder
    {
        public static IContainer Build()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<SettingsHandler>()
                .As<ISettingsHandler>()
                .SingleInstance()
                .WithParameter("appSettingsFile", "appsettings.json");

            builder.Register(c =>
            {
                var settings = c.Resolve<ISettingsHandler>().GetSettings();
                var client = new RestClient(settings.Url)
                {
                    Authenticator = OAuth1Authenticator.ForProtectedResource(
                        settings.ApiKey,
                        settings.ApiSecretKey,
                        settings.AccessToken,
                        settings.AccessTokenSecret
                    )
                };
                return client;
            }).As<IRestClient>().SingleInstance();

            builder.RegisterType<TwitterClient>().As<ITwitterRepo>();
            builder.RegisterType<TwitterParser>().As<ITwitterParser>();

            var container = builder.Build();

            return container;
        }
    }
}
