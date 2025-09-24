using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CQRS.Core.Messages
{
    public abstract class Message
    {
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
    }
}