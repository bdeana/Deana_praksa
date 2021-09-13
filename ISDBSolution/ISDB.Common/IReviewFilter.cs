namespace ISDB.Common
{
    public interface IReviewFilter
    {
        string Filter { get; set; }

        string FilterLike(string Filter);
    }
}