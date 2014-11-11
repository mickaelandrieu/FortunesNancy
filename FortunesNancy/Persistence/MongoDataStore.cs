namespace FortunesNancy
{
    using System.Collections.Generic;
    using System.Linq;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;

    public class MongoDataStore : IDataStore
    {
        private MongoDatabase database;
        private MongoCollection<BsonDocument> fortunes;

        public MongoDataStore(string connectionString)
        {
            database = MongoDatabase.Create(connectionString);
            fortunes = database.GetCollection("fortunes");
        }

        public long Count { get { return fortunes.FindAll().Count(); } }

        public IEnumerable<Fortune> GetAll()
        {
            return fortunes.FindAllAs<Fortune>().ToArray();
        }

        public bool TryAdd(Fortune fortune)
        {
            if (FindById(fortune.id) != null)
                return false;

            fortunes.Insert(fortune);
            return true;
        }

        public bool TryRemove(int id)
        {
            if (FindById(id) == null)
                return false;

            fortunes.Remove(Query.EQ("_id", id));
            return true;
        }

        private BsonDocument FindById(long id)
        {
            return fortunes.Find(Query.EQ("_id", id)).FirstOrDefault();
        }

        public bool TryUpdate(Fortune fortune)
        {
            if (FindById(fortune.id) == null)
                return false;

            fortunes.Save(fortune);
            return true;
        }
    }
}