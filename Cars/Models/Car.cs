using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cars.Models
{
    public class Car
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
