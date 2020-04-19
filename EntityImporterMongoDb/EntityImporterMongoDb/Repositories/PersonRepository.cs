using EntityImporterMongoDb.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityImporterMongoDb.Repositories
{
    public class PersonRepository
    {
        private const string Database = "config";
        private const string ConnectionString = "mongodb://myUserAdmin:abc123@localhost:27017";
        private readonly IMongoCollection<Person> _collection;

        public PersonRepository()
        {
            IMongoClient client = new MongoClient(MongoClientSettings.FromConnectionString(ConnectionString));

            BsonSerializer.RegisterSerializer(typeof(decimal),
                new DecimalSerializer(BsonType.Decimal128));

            BsonSerializer.RegisterSerializer(typeof(decimal?),
                new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128)));

            ConventionRegistry.Register("SicesConvention", new ConventionPack
            {
                new IgnoreIfNullConvention(true),
                new EnumRepresentationConvention(BsonType.Int32),
                new IgnoreExtraElementsConvention(true)
            }, t => true);

            _collection = client.GetDatabase(Database).GetCollection<Person>(nameof(Person));
        }

        public async Task BulkInsertAsync(List<Person> elements)
        {
            var count = elements.Count;
            var paging = Math.Ceiling(count / 10M);

            for (var i = 0; i < paging; i++)
            {
                Console.WriteLine($"################## INSERINDO {i * 10}/{count} ##################");
                await _collection.InsertManyAsync(elements);
            }

            await Task.CompletedTask;
        }

    }
}
