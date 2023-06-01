namespace DogsApp_DAL.Helpers
{
    public interface ISortHelper<T>
    {
        IQueryable<T> ApplySort(IQueryable<T> entities, string atribute, string order);
    }
}