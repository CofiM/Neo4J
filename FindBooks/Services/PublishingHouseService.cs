using FindBooks.Models;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindBooks.Services
{
    public class PublishingHouseService
    {
        public readonly IGraphClient client;

        public PublishingHouseService(IGraphClient client)
        {
            this.client = client;
        }

        public async Task AddHouseAsync(string name, string year, string email, string contact, string place, string pass)
        {
            var oldHouse = client.Cypher.Match("(n:PublishingHouse)")
                            .Where("n.Name='" + name + "'" + " AND n.Place =" + "'" +  place + "'")
                            //.Where("n.Name="+name + " AND n.Place=" + place)
                            .Return(n => n.As<PublishingHouse>()).ResultsAsync.Result.FirstOrDefault();

            if(oldHouse != null)
            {
                throw new InvalidOperationException("Publishing house already exist!");
            }

            PublishingHouse house = new PublishingHouse
            {
                Name = name,
                YearOfEstablishment = year,
                Email = email,
                Contact = contact,
                Place = place,
                Role = 'H',
                Password = pass
            };

            await client.Cypher.Create("(n:PublishingHouse $dept)")
                            .WithParam("dept", house)
                            .ExecuteWithoutResultsAsync();

        }

        public List<PublishingHouse> GetAllHouse()
        {
            List<PublishingHouse> houses = new List<PublishingHouse>();

            var result = client.Cypher.Match("(n : PublishingHouse)")
                                                .With("n{.*, Id:id(n)} as u")
                                                .Return(u => u.As<PublishingHouse>()).ResultsAsync.Result.ToList();

            foreach (var item in result)
            {
                houses.Add(item);
            }

            return houses;
        }

        public PublishingHouse GetPublishingHouse(int houseId)
        {
            var house = client.Cypher.Match("(n:PublishingHouse)")
                                    .Where("id(n)=" + houseId)
                                    .Return(n => n.As<PublishingHouse>())
                                    .ResultsAsync.Result.FirstOrDefault();
            return house;
        }
    }
}
