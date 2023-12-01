using FindBook.Core.Models;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindBooks.Services
{
    public class MarkService
    {
        public readonly IGraphClient client;

        public MarkService(IGraphClient client)
        {
            this.client = client;
        }

        public async Task<int> AddMarkAsync(float grade)
        {
            if (grade < 1 || grade > 5)
                throw new Exception("invalid grade");
            int wantedId;

            var markRes = await client.Cypher.Match("(n:Mark)")
                                         .Where("n.Number = $grade")
                                         .WithParam("grade",grade)
                                         .With("n{.*, Id:id(n)} as u")
                                         .Return(u => u.As<Mark>()).ResultsAsync;
            if(markRes.Count() != 0)
            {
                var mark = markRes.FirstOrDefault();
                wantedId = mark.Id;
            }
            else
            {
                Mark newMark = new Mark
                {
                    Number = grade
                };

                var idOfMarkTemp = client.Cypher.Create("(n:Mark $mark)")
                                            .WithParam("mark", newMark)
                                            .With("n{.*, Id:id(n)} as u")
                                            .Return(u => u.As<Mark>()).ResultsAsync;

                var markResult = idOfMarkTemp.Result.FirstOrDefault();
                wantedId = markResult.Id;
            }

            return wantedId;
        }

        public async Task MarkBook(int markId, string bookName)
        {
            var bookTemp = await client.Cypher.Match("(n:Book)")
                               .Where("n.Title=" + "'" + bookName +"'")
                                .With("n{.*, Id:id(n)} as u")
                                 .Return(u => u.As<Mark>()).ResultsAsync;

            var book = bookTemp.FirstOrDefault();

            var rated = client.Cypher.Match("(n:Mark), (m:Book)")
                                     .Where("id(n)="+markId+ " AND m.Title=" + "'" + bookName + "'")
                                     .Create("(n)<-[r:RATED]-(m)")
                                     .ExecuteWithoutResultsAsync();
        }

        public async Task MarkUser(int markId, int userId)
        {
            var userTemp = await client.Cypher.Match("(n:User)")
                             .Where("id(n) = $userId")
                             .WithParam("userId", userId)
                              .With("n{.*, Id:id(n)} as u")
                               .Return(u => u.As<Mark>()).ResultsAsync;

            var user = userTemp.FirstOrDefault();
            var posted = client.Cypher.Match("(n:Mark), (m:User)")
                               .Where("id(n)=" + markId + " AND id(m)=" + userId)
                               .Create("(n)<-[r:POSTED]-(m)")
                               .ExecuteWithoutResultsAsync();
        }

        public async Task BookUser(string bookName,int userId)
        {
            var posted = client.Cypher.Match("(n:Book), (m:User)")
                         .Where("n.Title = " + "'" + bookName + "'" + " AND id(m)=" + userId)
                         .Create("(n)<-[r:READ]-(m)")
                         .ExecuteWithoutResultsAsync();
        }

        public async Task DeleteMarkAsync(int markId)
        {
            await client.Cypher.Match("(p:Mark)")
                                .Where("id(p) = $markId")
                                .WithParam("markId", markId)
                                .Delete("p")
                                .ExecuteWithoutResultsAsync();
        }

        public async Task<float> GetUsersMarkForBook(int userId, int bookId)
        {
            var markResult = await client.Cypher.Match("(n:Mark),(m:User),(k:Book), (n)<-[r:POSTED]-(m), (n)<-[r:RATED]-(k)")
                                .Where("id(m)="+userId+" AND id(k)="+bookId)
                                .With("n{.*, Id:id(n)} as u")
                                .Return(u => u.As<Mark>()).ResultsAsync;
            var mark = markResult.FirstOrDefault();
            return mark.Id;
            //vraca tip mark takodje i njegov kontroller nzm za front kako ce bude
        }

        public async Task<float> GetAverageMarkForBook(int bookId)
        {
            var markResult = await client.Cypher.Match("(n:Mark),(m:Book),(n)<-[r:RATED]-(m)")
                    .Where("id(m)=" + bookId)
                    .Return(n => n.As<Mark>()).ResultsAsync;
            var marks = markResult.ToList();

            float sum = 0;
            float count = 0;
            foreach(var mark in marks)
            {
                sum += mark.Number;
                count++;
            }

            return sum / count;
        }
    }
}
