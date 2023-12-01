using FindBook.Core.Models;
using FindBooks.Models;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace FindBooks.Services
{
    public class UserService
    {
        public readonly IGraphClient client;

        public UserService(IGraphClient client)
        {
            this.client = client;
        }

        public async Task<User> CreateAccount(string username, string mail, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                return null;
            }


            var userAccount = new User
            {
                Username = username,
                Mail = mail,
                Password = password,
                Role = 'U'
            };

            await client.Cypher
                        .Create("(n:User $dept)")
                        .WithParam("dept", userAccount)
                        .ExecuteWithoutResultsAsync();

            return userAccount;
        }

        public async Task<object> GetAccount(string username, string password)
        {
            var user = (await client.Cypher.Match("(d:User)")
                                                .Where((User d) => d.Username == username && d.Password == password)
                                                .With("d{.*, Id:id(d)} as u")
                                                .Return(u => u.As<User>()).ResultsAsync).FirstOrDefault();

            if (user != null)
            {
                return new User 
                { 
                    Id = user.Id,
                    Username = user.Username,
                    Mail = user.Mail,
                    Password = user.Password,
                    Role = user.Role
                };
            }

            var house = (await client.Cypher.Match("(d:PublishingHouse)")
                                                .Where((PublishingHouse d) => d.Name == username && d.Password == password)
                                                .With("d{.*, Id:id(d)} as u")
                                                .Return(u => u.As<PublishingHouse>()).ResultsAsync).FirstOrDefault();

            if (house != null)
            {
                return new PublishingHouse
                {
                    Id = house.Id,
                    Name = house.Name,
                    YearOfEstablishment = house.YearOfEstablishment,
                    Email = house.Email,
                    Password = house.Password,
                    Contact = house.Contact,
                    Place = house.Place,
                    Role = house.Role
                };
            }

            throw new Exception("Accoutn not found!");
        }

        public object UserReadBook(int bookId, int userId)
        {
            var user = client.Cypher.Match("(n:User)")
                                    .Where("id(n)=" + userId)
                                    .Return(n => n.As<User>())
                                    .ResultsAsync.Result.FirstOrDefault();

            var book = client.Cypher.Match("(n:Book)")
                                    .Where("id(n)=" + bookId)
                                    .Return(n => n.As<Book>())
                                    .ResultsAsync.Result.FirstOrDefault();

            var result = client.Cypher.Match("(n:Book), (m:User)")
                                .Where("id(n)=" + bookId + " AND id(m)=" + userId)
                                .Create("(m)-[r:READ]->(n)")
                                .Return((n, m) => new
                                {
                                    Book = n.As<Book>(),
                                    User = m.As<User>()
                                });
            return result;

        }

    }
}
