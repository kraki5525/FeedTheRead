using System.Configuration;
using System.Data.Common;
using System.Data.SQLite;
using FeedTheRead.Models;
using Nancy;

namespace FeedTheRead
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(Nancy.TinyIoc.TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["data"];
            var providerFactory = new SQLiteFactory();
            var repository = new Repository(providerFactory, connectionString.ConnectionString);
            container.Register(typeof(IRepository), repository);

            base.ApplicationStartup(container, pipelines);
        }
    }
}