using FindBook.Core.Models;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindBooks.Services
{
    public class GenreService
    {
        public readonly IGraphClient client;

        public GenreService(IGraphClient client)
        {
            this.client = client;
        }
        public int AddGenreAsync(string name)
        {
            int id;

            var temp = client.Cypher.Match("(n:Genre)").Where("n.Name=" + "'" + name + "'")
                                                        .With("n{.*, Id:id(n)} as u")
                                                        .Return(u => u.As<Genre>()).ResultsAsync.Result;
            var tmp = temp.FirstOrDefault();

            if (temp.Count() != 0)
            {
                id = tmp.Id;
            }
            else
            {

                var genre = new Genre
                {
                    Id = 0,
                    Name = name
                };

                var some = client.Cypher
                      .Create("(n:Genre $dept)")
                      .WithParam("dept", genre)
                      .With("n{.*, Id:id(n)} as u")
                       .Return(u => u.As<Genre>()).ResultsAsync.Result;
                var som = some.FirstOrDefault();
                id = som.Id;
            }
            return id;
        }

        public List<Genre> GetAllGenre()
        {
            List<Genre> genres = new List<Genre>();

            var result = client.Cypher.Match("(n : Genre)")
                                                .With("n{.*, Id:id(n)} as u")
                                                .Return(u => u.As<Genre>()).ResultsAsync.Result;


            var tmp = result.ToList();

            foreach (var item in tmp)
            {
                genres.Add(item);
            }

            return genres;

        }

        public Genre GetGenreName(string name)
        {
            var genre = client.Cypher.Match("(n:Genre)").Where("n.Name = '" + name + "'").Return(n => n.As<Genre>()).ResultsAsync.Result;
            var tmp = genre.FirstOrDefault();


            return tmp;
        }


    }
}