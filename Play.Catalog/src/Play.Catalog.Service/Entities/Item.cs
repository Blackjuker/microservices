using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Play.Common;

namespace Play.Catalog.Service.Entities
{

    public class Item : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [BsonRepresentation(BsonType.String)]
        public decimal Price { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}