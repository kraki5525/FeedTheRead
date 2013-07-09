using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using FeedTheRead.Models;
using FeedTheRead.Models.Queries;
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
            var rep = A.Fake<IRepository>();
            var browser = new Browser(with =>
                {
                    with.RootPathProvider<TestRootPathProvider>();
                    with.Module(new HomeModule(rep));
                });

            var result = browser.Get("/", with => with.HttpRequest());

            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        
        [Test]
        public void DefaultContainsAFeedSection()
        {
            var rep = A.Fake<IRepository>();
            var browser = new Browser(with =>
                {
                    with.RootPathProvider<TestRootPathProvider>();
                    with.Module(new HomeModule(rep));
                });

            var result = browser.Get("/", with => with.HttpRequest());

            result.Body["#feed-list"].ShouldExistOnce();
        }

        [Test]
        public void FeedSectionContainsFeedsFromDatabase()
        {
            var rep = A.Fake<IRepository>();
            var feeds = new[]
                {
                    new Feed() {Id = 1, Name = "Test Feed", Uri = "http://test.com"},
                    new Feed() {Id = 2, Name = "Test 2", Uri = "http://test2.com"}
                };
            A.CallTo(() => rep.Query(A<GetFeedsQuery>.Ignored)).Returns(feeds);

            var browser = new Browser(with =>
                {
                    with.RootPathProvider<TestRootPathProvider>();
                    with.Module(new HomeModule(rep));
                });

            var result = browser.Get("/", with => with.HttpRequest());

            result.Body["#feed-list .feed"].ShouldExistExactly(feeds.Length);
        }
    }
}