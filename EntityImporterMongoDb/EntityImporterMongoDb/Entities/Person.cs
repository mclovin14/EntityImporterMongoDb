using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace EntityImporterMongoDb.Entities
{
    public class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Document Document { get; set; }
    }
}
