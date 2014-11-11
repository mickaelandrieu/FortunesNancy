namespace FortunesNancy
{
    using System.Collections.Generic;

    public interface IDataStore
    {
        IEnumerable<Fortune> GetAll();
        long Count { get; }
        bool TryAdd(Fortune fortune);
        bool TryRemove(int id);
        bool TryUpdate(Fortune fortune);
    }
}