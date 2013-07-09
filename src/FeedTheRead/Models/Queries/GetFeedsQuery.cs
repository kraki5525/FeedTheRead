using System.Collections.Generic;
using System.Data;

//using Dapper;
using FeedTheRead.Models.Dapper;

namespace FeedTheRead.Models.Queries
{
    public class GetFeedsQuery : IQuery<IEnumerable<Feed>>
    {
         public IEnumerable<Feed> Execute(IDbConnection connection)
         {
             return connection.Query<Feed>("select Id,Name,Uri from Feed");
         }
    }
}