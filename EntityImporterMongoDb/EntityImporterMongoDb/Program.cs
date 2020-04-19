using EntityImporterMongoDb.Repositories;
using System;
using System.Threading.Tasks;

namespace EntityImporterMongoDb
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine("################## LENDO ARQUIVO JSON ##################");

            var person = await Task.Run(() => new JsonRepository().FindPersons());

            Console.WriteLine($"################## {person.Count} PESSOAS ENCONTRADAS. ##################");

            await new PersonRepository().BulkInsertAsync(person);

            Console.WriteLine("################## PROCESSO DE LEITURA DO JSON FINALIZADO ##################");
        }

    }
}
