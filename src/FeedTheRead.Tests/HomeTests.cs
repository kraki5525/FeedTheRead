using FeedTheRead.Modules;
using FluentAssertions;
using NUnit.Framework;
using Nancy;
using Nancy.Testing;

namespace FeedTheRead.Tests
{
    [TestFixture]
    public class HomeTests
    {
        [Test]
        public void DefaultReturnsStatusOk()
        {
            var browser = new Browser(with =>
                {
                    with.Module<HomeModule>();
                });

            var result = browser.Get("/", with =>
            {
                with.HttpRequest();
            });

            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        
        [Test]
        public void DefaultContainsAFeedSection()
        {
            var browser = new Browser(with =>
                {
                    with.Module<HomeModule>();
                });

            var result = browser.Get("/", with => with.HttpRequest());

            result.Body["#feed-list"].ShouldExistOnce();
        }

    }
}