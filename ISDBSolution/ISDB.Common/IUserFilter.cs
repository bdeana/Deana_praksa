namespace ISDB.Common
{
    public interface IUserFilter
    {
        string Filter { get; set; }

        string FilterLike(string Filter);
    }
}