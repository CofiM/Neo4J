using FindBook.Core.Models;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindBooks.Services
{
    public class WriterService
    {
        public readonly IGraphClient client;

        public WriterService(IGraphClient client)
        {
            this.client = client;
        }

        public async Task AddAsync(string firstname, string lastname, string birthPlace, string birthYear, string yearOfDeath, string biography)
        {
            Writer writer = new Writer
            {
                Firstname = firstname,
                Lastname = lastname,
                Birthplace = birthPlace,
                BirthYear = birthYear,
                YearOfDeath = yearOfDeath,
                Biography = biography
            };

            await client.Cypher.Create("(n:Writer $dept)")
                .WithParam("dept", writer)
                .ExecuteWithoutResultsAsync();

        }

        public Writer GetWriterById(int id)
        {
            var writer = client.Cypher.Match("(w:Writer)")
                           .Where("id(w)=" + id)
                           .Return(w => w.As<Writer>()).ResultsAsync.Result;
            
            return writer.First();
        }

        public Writer GetWriterByName(string firstname, string lastname)
        {
            var writer = client.Cypher.Match("(w:Writer)")
                           .Where("w.Firstname='" + firstname + "' AND w.Lastname='" + lastname + "'")
                           .Return(w => w.As<Writer>()).ResultsAsync.Result;

            return writer.First();
        }


        public List<Writer> GetAllWriter()
        {
            List<Writer> genres = new List<Writer>();

            var result = client.Cypher.Match("(n:Writer)")
                                                .With("n{.*, Id:id(n)} as u")
                                                .Return(u => u.As<Writer>()).ResultsAsync.Result;


            var tmp = result.ToList();

            foreach (var item in tmp)
            {
                genres.Add(item);
            }

            return genres;

        }
    }
}
