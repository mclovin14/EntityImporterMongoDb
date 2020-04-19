using EntityImporterMongoDb.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using static EntityImporterMongoDb.Utils.PropertyName;

namespace EntityImporterMongoDb.Utils
{
    public class JsonStreamReaderUtf8
    {
        public List<Person> Handle (string json)
        {
            ReadOnlySpan<byte> jsonUtf8 = Encoding.UTF8.GetBytes(json);
            var reader = new Utf8JsonReader(jsonUtf8, true, default);
            return ParseObject(ref reader);
        }

        private static List<Person> ParseObject(ref Utf8JsonReader reader)
        {
            var persons = new List<Person>();

            while(reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                var pinto = ParsePersonArray(ref reader);
                persons.Add(pinto);
            }

            return persons;
        }

        private static Person ParsePersonArray(ref Utf8JsonReader reader)
        {
            var person = new Person();


            while(reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                var propertyName = reader.ValueSpan;
                ParsePersonProperty(ref reader, propertyName, person);
            }
            return person;
        }

        private static Document ParseDocumentArray(ref Utf8JsonReader reader)
        {
            var document = new Document();
            reader.Read();

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                var propertyName = reader.ValueSpan;
                ParseDocumentProperty(ref reader, propertyName, document);
            }
            return document;
        }

        private static void ParsePersonProperty(ref Utf8JsonReader reader, in ReadOnlySpan<byte> property, Person person)
        {
            if (property.SequenceEqual(ID))
            {
                reader.Read();
                person.Id = Guid.Parse(reader.GetString());
            }
            else if (property.SequenceEqual(NAME))
            {
                reader.Read();
                person.Name = reader.GetString();
            }
            else if (property.SequenceEqual(DOCUMENT))
            {
                person.Document = ParseDocumentArray(ref reader);
            }
        }

        private static void ParseDocumentProperty(ref Utf8JsonReader reader, in ReadOnlySpan<byte> property, Document document)
        {
            if (property.SequenceEqual(DOCUMENTNUMBER))
            {
                reader.Read();
                document.Number = reader.GetInt32();
            }
            else if (property.SequenceEqual(DOCUMENTTYPE))
            {
                reader.Read();
                document.Type = reader.GetString();
            }           
        }
    }
}
