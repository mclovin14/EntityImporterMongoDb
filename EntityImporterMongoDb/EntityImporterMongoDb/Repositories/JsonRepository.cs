using EntityImporterMongoDb.Entities;
using EntityImporterMongoDb.Utils;
using System.Collections.Generic;
using System.IO;

namespace EntityImporterMongoDb.Repositories
{
    public class JsonRepository
    {
        public List<Person> FindPersons()
        {
            var persons = new List<Person>();
            var files = new List<string> {
                new string(@"C:\Users\Barbieri\Documents\github\EntityImporterMongoDb\EntityImporterMongoDb\EntityImporterMongoDb\JsonImported\person_teste.json")
            };

            foreach (var file in files)
            {
                var json = File.ReadAllText(file);
                persons.AddRange(new JsonStreamReaderUtf8().Handle(json));
            }

            return persons;
        }

    }
}
