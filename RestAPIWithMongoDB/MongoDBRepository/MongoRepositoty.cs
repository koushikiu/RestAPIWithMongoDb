using MongoDB.Bson;
using MongoDB.Driver;
using RestAPIWithMongoDB.DataModel;

namespace RestAPIWithMongoDB.MongoDBRepository
{
    public class MongoRepositoty : IMongoRepository
    {
        private readonly IMongoCollection<User> userCollection;
        private readonly FilterDefinitionBuilder<User> filterBuilder = Builders<User>.Filter;
        public MongoRepositoty(IMongoClient mongoClient)
        {
            IMongoDatabase mongoDatabase = mongoClient.GetDatabase("User");
            userCollection = mongoDatabase.GetCollection<User>("User");
        }

        public async Task CreateUserAsync(User user)
        {
           await userCollection.InsertOneAsync(user);
        }

        public User GetUser(Guid guid)
        {
            var filter = filterBuilder.Eq(user => user.Id, guid);
            return userCollection.Find(filter).SingleOrDefault();
        }

        public  IEnumerable<User> GetUsers()
        {
            return  userCollection.Find(new BsonDocument()).ToList();
        }

        public async Task UpdateUser(User user)
        {
            var filter = filterBuilder.Eq(u => u.Id, user.Id);
           await userCollection.ReplaceOneAsync(filter, user);
        }

        public async Task DeleteUser(Guid id)
        {
            var filter = filterBuilder.Eq(u => u.Id, id);
            await userCollection.DeleteOneAsync(filter);
        }
    }
}
public interface IMongoRepository
{
    IEnumerable<User> GetUsers();
    User GetUser(Guid guid);
    Task CreateUserAsync(User user);
    Task UpdateUser(User user);
    Task DeleteUser(Guid id);   
}
