namespace FeedTheRead.Models
{
    public interface IRepository
    {
        T Query<T>(IQuery<T> query);
    }
}