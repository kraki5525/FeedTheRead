using System.Linq;
using FeedTheRead.Models;
using FeedTheRead.Models.Queries;
using Nancy;

namespace FeedTheRead.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule(IRepository repository)
        {
            Get["/"] = p =>
                {
                    var feeds = repository.Query(new GetFeedsQuery()).ToArray();

                    return Negotiate
                        .WithModel(feeds)
                        .WithView("home.cshtml");
                };
        }
    }
}