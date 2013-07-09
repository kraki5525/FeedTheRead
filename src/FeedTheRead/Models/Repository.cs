using System;
using System.Data.Common;

namespace FeedTheRead.Models
{
    public class Repository : IRepository
    {
        private readonly DbProviderFactory dbFactory;
        private readonly string dbConnectionString;

        public Repository(DbProviderFactory factory, string connectionString)
        {
            dbFactory = factory;
            dbConnectionString = connectionString;
        }

        public T Query<T>(IQuery<T> query)
        {
            using (var connection = dbFactory.CreateConnection())
            {
                if (connection == null)
                    throw new NullReferenceException("connection is null");

                connection.ConnectionString = dbConnectionString;
                connection.Open();
                return query.Execute(connection);
            }
        }
    }
}