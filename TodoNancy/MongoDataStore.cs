namespace TodoNancy
{
    using System.Collections.Generic;
    using System.Linq;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;

    public class MongoDataStore : IDataStore
    {
        private MongoClient mongoClient;
        private MongoCollection<Todo> todos;

        public MongoDataStore(string connectionString)
        {
            mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetServer().GetDatabase("todos");
            todos = database.GetCollection<Todo>("todos");
        }

        public long Count { get { return todos.FindAll().Count(); } }

        public IEnumerable<Todo> GetAll()
        {
            return todos.FindAllAs<Todo>();
        }

        public bool TryAdd(Todo todo)
        {
            if (FindById(todo.id) != null)
                return false;

            todos.Insert(todo);
            return true;
        }

        public bool TryRemove(int id)
        {
            if (FindById(id) == null)
                return false;

            todos.Remove(Query<Todo>.EQ(e => e.id, id));
            return true;
        }

        private Todo FindById(long id)
        {
            var query = Query<Todo>.EQ(e => e.id, id);
            return todos.FindOne(query);
        }

        public bool TryUpdate(Todo todo)
        {
            if (FindById(todo.id) == null)
                return false;

            todos.Save(todo);
            return true;
        }
    }
}