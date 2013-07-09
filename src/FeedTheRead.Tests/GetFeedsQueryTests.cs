using System.Data;

using FakeItEasy;

using FeedTheRead.Models.Queries;

using FluentAssertions;

using NUnit.Framework;

namespace FeedTheRead.Tests
{
    [TestFixture]
    public class GetFeedsQueryTests
    {
        [Test]
        public void ExecuteCallsConnection()
        {
            var connection = A.Fake<IDbConnection>();
            var command = A.Fake<IDbCommand>();
            var reader = A.Fake<IDataReader>();
            A.CallTo(() => connection.State).Returns(ConnectionState.Open);
            A.CallTo(() => reader.FieldCount).Returns(3);
            A.CallTo(() => reader.GetName(0)).Returns("Id");
            A.CallTo(() => reader.GetName(1)).Returns("Name");
            A.CallTo(() => reader.GetName(2)).Returns("Uri");
            A.CallTo(() => command.ExecuteReader(A<CommandBehavior>.Ignored)).Returns(reader);
            A.CallTo(() => connection.CreateCommand()).Returns(command);

            var query = new GetFeedsQuery();
            query.Execute(connection);

            command.CommandText.ToLower().Should().Be("select id,name,uri from feed");
        }
        
        [Test]
        public void CommandIsNotInTransaction()
        {
            var connection = A.Fake<IDbConnection>();
            var command = A.Fake<IDbCommand>();
            var reader = A.Fake<IDataReader>();
            command.Transaction = null;
            A.CallTo(() => connection.State).Returns(ConnectionState.Open);
            A.CallTo(() => reader.FieldCount).Returns(3);
            A.CallTo(() => reader.GetName(0)).Returns("Id");
            A.CallTo(() => reader.GetName(1)).Returns("Name");
            A.CallTo(() => reader.GetName(2)).Returns("Uri");
            A.CallTo(() => command.ExecuteReader(A<CommandBehavior>.Ignored)).Returns(reader);
            A.CallTo(() => connection.CreateCommand()).Returns(command);

            var query = new GetFeedsQuery();
            query.Execute(connection);

            command.Transaction.Should().BeNull();
        }
    }
}