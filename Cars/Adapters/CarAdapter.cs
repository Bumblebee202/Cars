using Cars.Database;
using Cars.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cars.Adapters
{
    public class CarAdapter : ICarAdapter
    {
        readonly MongoClient _client;
        readonly IMongoDatabase _database;

        public CarAdapter()
        {
            string connectionString = "mongodb://localhost";
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase("Cars");
        }

        public IMongoCollection<Car> Cars
        {
            get => _database.GetCollection<Car>("Cars");
        }

        public async Task<Car> Create(string name, string description)
        {
            Car car = new Car()
            {
                Name = name,
                Description = description
            };

            await Cars.InsertOneAsync(car);
            return car;
        }

        public async Task Delete(string id) => await Cars.DeleteOneAsync(c => c.Id.Equals(id));

        public async Task<IEnumerable<Car>> Enumerate() => await Cars.Find(new BsonDocument()).ToListAsync();

        public async Task<Car> Get(string id) => await Cars.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();

        public async Task<Car> Update(string id, string name, string description)
        {
            Car car = new Car
            {
                Id = id,
                Name = name,
                Description = description
            };
            await Cars.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(car.Id)), car);
            return car;
        }

        public async Task Update<T>(string id, string fieldName, T value)
        {
            IMongoCollection<BsonDocument> collection = _database.GetCollection<BsonDocument>("Cars");

            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            UpdateDefinition<BsonDocument> update = Builders<BsonDocument>.Update.Set(fieldName, value);
            await collection.UpdateOneAsync(filter, update);
        }
    }
}