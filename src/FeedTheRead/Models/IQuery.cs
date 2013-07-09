using System.Data;
using System.Data.Common;

namespace FeedTheRead.Models
{
    public interface IQuery<out T>
    {
        T Execute(IDbConnection connection);
    }
}