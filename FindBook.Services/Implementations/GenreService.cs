
using FindBook.Core.Models;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindBook.Services.Implementations
{
    public class GenreService
    {
        public readonly IGraphClient client;

        public GenreService(IGraphClient client)
        {
            this.client = client;
        }
        public async Task AddGenreAsync(string name)
        {
            var genre = new Genre
            {
                Name = name
            };

            await client.Cypher
                    .Create("(n:Genre $dept)")
                    .WithParam("dept", genre)
                    .ExecuteWithoutResultsAsync();

        }
    }
}
