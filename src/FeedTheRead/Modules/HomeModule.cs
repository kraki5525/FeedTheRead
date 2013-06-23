using Nancy;

namespace FeedTheRead.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = p =>
                {
                    return Negotiate
                        .WithView("home.cshtml");
                };
        }
    }
}