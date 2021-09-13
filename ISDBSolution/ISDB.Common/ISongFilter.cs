namespace ISDB.Common
{
    public interface ISongFilter
    {
        string Filter { get; set; }

        string FilterLike(string Filter);
    }
}