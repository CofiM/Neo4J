using FindBook.Core.Models;
using FindBooks.Models;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindBooks.Services
{
    public class BookService
    {
        public readonly IGraphClient client;

        public BookService(IGraphClient client)
        {
            this.client = client;
        }

        public List<Book> GetAllBookAsync()
        {
            List<Book> books = new List<Book>();

            var result = client.Cypher.Match("(n:Book)")
                                    .Return(n => n.As<Book>()).ResultsAsync.Result;

            var tmp = result.ToList();

            foreach (var item in tmp)
            {
                books.Add(item);
            }

            return books;
        }

        public List<Book> GetBookByPublishingHouse(int houseId)
        {
            var house = client.Cypher.Match("(n:PublishingHouse)")
                                    .Where("id(n)=" + houseId)
                                    .Return(n => n.As<PublishingHouse>())
                                    .ResultsAsync.Result.FirstOrDefault();

            if (house == null)
            {
                throw new InvalidOperationException("Publishing house does not exist!");
            }

            var books = client.Cypher.Match("(n:Book), (k:PublishingHouse), (k)-[r:PUBLISH]->(n)")
                            .Where("id(k)=" + houseId)
                            .Return((n) => n.As<Book>()).ResultsAsync.Result;

            List<Book> arrayOfBook = new List<Book>();
            foreach (var item in books)
            {
                var book = new Book
                {
                    Title = item.Title,
                    YearOfPublication = item.YearOfPublication,
                    Description = item.Description
                };
                arrayOfBook.Add(book);
            }
            return arrayOfBook;

        }

        public void PublishBook(int houseId, int bookId)
        {
            var house = client.Cypher.Match("(n:PublishingHouse)")
                                    .Where("id(n)=" + houseId)
                                    .Return(n => n.As<PublishingHouse>())
                                    .ResultsAsync.Result.FirstOrDefault();

            var book = client.Cypher.Match("(n:Book)")
                                    .Where("id(n)=" + bookId)
                                    .Return(n => n.As<Book>())
                                    .ResultsAsync.Result.FirstOrDefault();

            if(house == null || book == null)
            {
                throw new InvalidOperationException("House or book does not exist!");
            }

            client.Cypher.Match("(n:Book), (m:PublishingHouse)")
                        .Where("id(n)=" + bookId + " AND id(m)=" + houseId)
                        .Create("(m)-[r:PUBLISH]->(n)")
                        .ExecuteWithoutResultsAsync();

        }

        public void ReadBook(string bookName, int userId)
        {

            var book = client.Cypher.Match("(n:Book)")
                                    .Where("n.Title=" + "'" + bookName + "'")
                                    .Return(n => n.As<Book>())
                                    .ResultsAsync.Result.FirstOrDefault();

            var user = client.Cypher.Match("(n:User)")
                                    .Where("id(n)=" + userId)
                                    .Return(n => n.As<User>())
                                    .ResultsAsync.Result.FirstOrDefault();

            if(book == null || user == null)
            {
                throw new InvalidOperationException("User or book does not exist!");
            }

            client.Cypher.Match("(n:Book), (m:User)")
                        .Where("n.Title = " + "'" + bookName + "'" + " AND id(m)=" + userId)
                        .Create("(m)-[r:READ]->(n)")
                        .ExecuteWithoutResultsAsync();
        }

        public List<Book> GetBooksReadByUser(int userId)
        {
            var user = client.Cypher.Match("(n:User)")
                                    .Where("id(n)=" + userId)
                                    .Return(n => n.As<User>())
                                    .ResultsAsync.Result.FirstOrDefault();
            if(user == null)
            {
                throw new InvalidOperationException("User does not exist!");
            }

            var books = client.Cypher.Match("(n:Book), (k:User), (k)-[r:READ]->(n)")
                            .Where("id(k)=" + userId)
                            .Return((n) => n.As<Book>()).ResultsAsync.Result;

            List<Book> arrayOfBook = new List<Book>();
            foreach (var item in books)
            {
                var book = new Book
                {
                    Title = item.Title,
                    YearOfPublication = item.YearOfPublication,
                    Description = item.Description
                };
                arrayOfBook.Add(book);
            }
            return arrayOfBook;

        }

        public List<Book> GetBooksByGenre(string nameGenre, int userId)
        {
            var genre = client.Cypher.Match("(n:Genre)")
                                     .Where("n.Name='" + nameGenre + "'")
                                     .Return(n => n.As<Genre>())
                                     .ResultsAsync.Result.FirstOrDefault();

            //var genre = genres.First();


            var user = client.Cypher.Match("(n:User)")
                                    .Where("id(n)=" + userId)
                                    .Return(n => n.As<User>())
                                    .ResultsAsync.Result.FirstOrDefault();



            if (genre == null || user == null)
            {
                throw new InvalidOperationException("Genre or user does not exist!");
            }

            var books = client.Cypher.Match("(n:Book), (k:Genre), (m:User), (n)-[r:ASSIGN]->(k), (m)-[q:READ]->(n)")
                                .Where("k.Name='" + nameGenre + "'")
                                .Return((n) => n.As<Book>()).ResultsAsync.Result;


            List<Book> arrayOfBook = new List<Book>();
            foreach(var item in books)
            {
                var book = new Book
                {
                    Title = item.Title,
                    YearOfPublication = item.YearOfPublication,
                    Description = item.Description
                    //Genre = genre
                };
                arrayOfBook.Add(book);
            }
            return arrayOfBook;
        }

        public object GetBook(int id)
        {
            var book = client.Cypher.Match("b:Book-[r:ASSIGN]->g:Genre, " + " w:Writer-[q:WROTE]->b:Book")
                            .Where("id(b)=" + id)
                            .Return((b, g, w) => new
                            {
                                Book = b.As<Book>(),
                                Genre = g.As<Genre>(),
                                Writer = w.As<Writer>()
                            }).ResultsAsync.Result;
            return book;
        }

        public object GetBookByBookName(string name)
        {
            var book = client.Cypher.Match("(b:Book)-[r:ASSIGN]->(g:Genre), " + " (w:Writer)-[q:WROTE]->(b:Book)")
                            .Where("b.Title='" + name + "'")
                            .Return((b, g, w) => new
                            {
                                Book = b.As<Book>(),
                                Genre = g.As<Genre>(),
                                Writer = w.As<Writer>()
                            }).ResultsAsync.Result;
            return book;
        }

        //Mislim da nece da treba
        public int AddBook(string nameBook, string year, string description, int genreId, int writerId)
        {
            var genres = client.Cypher.Match("(n:Genre)")
                                    .Where("id(n)=" + genreId)
                                    .Return(n => n.As<Genre>())
                                    .ResultsAsync.Result;

            var genre = genres.FirstOrDefault();

            var writerRes = client.Cypher.Match("(n:Writer)")
                                         .Where("id(n)=" + writerId)
                                         .With("n{.*, Id:id(n)} as u")
                                         .Return(u => u.As<Writer>()).ResultsAsync.Result;

            var writer = writerRes.FirstOrDefault();

            if( genre == null || writer == null )
            {
                throw new InvalidOperationException("Genre or writer does not exist!");
            }

            Book newBook = new Book
            {
                Id = 0,
                Title = nameBook,
                YearOfPublication = year,
                Description = description
            };

            var findBook = client.Cypher.Match("(n:Book)")
                                     .Where("n.Title=" + "'" + nameBook + "'")
                                     .Return(n => n.As<Book>())
                                     .ResultsAsync.Result.FirstOrDefault();

            if (findBook != null)
            {
                throw new InvalidOperationException("Book already exist!");
            }


            var bookRes = client.Cypher.Create("(k:Book $book)")
                        .WithParam("book", newBook)
                        .With("k{.*, Id:id(k)} as u")
                        .Return(u => u.As<Book>()).ResultsAsync.Result;

            var book = bookRes.FirstOrDefault();

            int id = book.Id;


            var res1 = client.Cypher.Match("(n:Book), (m:Writer), (k:Genre)")
                                .Where("id(n)=" + book.Id + " AND id(m)=" + writerId + " AND id(k)=" + genreId)
                                .Create("(m)-[r:WROTE]->(n)," + " (n)-[q:ASSIGN]->(k) ")
                                .ExecuteWithoutResultsAsync();

            return id;
        }

        //Mislim da nece da treba
        public int AddBookAndWriter(string title, string yearOfPublication, string description, int idGenre,
                            string firstname, string lastname, string birthPlace, string birthYear, string yearOfDeath, string biography)
        {
            var genres = client.Cypher.Match("(n:Genre)")
                                    .Where("id(n)=" + idGenre)
                                    .Return(n => n.As<Genre>())
                                    .ResultsAsync.Result;

            var genre = genres.FirstOrDefault();

            if (genre == null)
            {
                throw new InvalidOperationException("Genre or writer does not exist!");
            }

            var findBook = client.Cypher.Match("(n:Book)")
                                     .Where("n.Title=" + "'" + title + "'")
                                     .Return(n => n.As<Book>())
                                     .ResultsAsync.Result.FirstOrDefault();

            if (findBook != null)
            {
                throw new InvalidOperationException("Book already exist!");
            }

            Book newBook = new Book
            {
                Id = 0,
                Title = title,
                YearOfPublication = yearOfPublication,
                Description = description
            };


            var bookRes = client.Cypher.Create("(k:Book $book)")
                        .WithParam("book", newBook)
                        .With("k{.*, Id:id(k)} as u")
                        .Return(u => u.As<Book>()).ResultsAsync.Result;

            var book = bookRes.FirstOrDefault();
            int id = book.Id;

            Writer newWriter = new Writer
            {
                Firstname = firstname,
                Lastname = lastname,
                Birthplace = birthPlace,
                BirthYear = birthYear,
                YearOfDeath = yearOfDeath,
                Biography = biography
            };

            var writerRes = client.Cypher.Create("(k:Writer $writer)")
                        .WithParam("writer", newWriter)
                        .With("k{.*, Id:id(k)} as u")
                        .Return(u => u.As<Writer>()).ResultsAsync.Result;

            var writer = writerRes.FirstOrDefault();
            var writerId = writer.Id;

            var res1 = client.Cypher.Match("(n:Book), (m:Writer), (k:Genre)")
                                .Where("id(n)=" + book.Id + " AND id(m)=" + writer.Id + " AND id(k)=" + idGenre)
                                .Create("(m)-[r:WROTE]->(n)," + " (n)-[q:ASSIGN]->(k) ")
                                .ExecuteWithoutResultsAsync();

            return book.Id;
            
        }


        public int AddBookByPublishingHouse(int houseId, string title, string yearOfPublication, string description, int idGenre,
                            string firstname, string lastname, string birthPlace, string birthYear, string yearOfDeath, string biography)
        {
            var genre = client.Cypher.Match("(n:Genre)")
                                    .Where("id(n)=" + idGenre)
                                    .Return(n => n.As<Genre>())
                                    .ResultsAsync.Result.FirstOrDefault();

            if (genre == null)
            {
                throw new InvalidOperationException("Genre does not exist!");
            }


            var book = client.Cypher.Match("(n:Book), (k:PublishingHouse), (k)-[r:PUBLISH]->(n)")
                       .Where("n.Title=" + "'" + title + "'" + " AND id(k)=" + houseId)
                       .Return(n => n.As<Book>()).ResultsAsync.Result.FirstOrDefault();


            if (book != null)
            {
                throw new InvalidOperationException("Book already exist!");
            }

            Book newBook = new Book
            {
                Id = 0,
                Title = title,
                YearOfPublication = yearOfPublication,
                Description = description
            };


            var bookRes = client.Cypher.Create("(k:Book $book)")
                        .WithParam("book", newBook)
                        .With("k{.*, Id:id(k)} as u")
                        .Return(u => u.As<Book>()).ResultsAsync.Result;
            var book1 = bookRes.FirstOrDefault();
            var bookID = book1.Id;

            var writer = client.Cypher.Match("(w:Writer)")
                          .Where("w.Firstname='" + firstname + "' AND w.Lastname='" + lastname + "'")
                          .With("w{.*, Id:id(w)} as u")
                          .Return(u => u.As<Writer>()).ResultsAsync.Result.FirstOrDefault();

            if(writer != null)
            {
                var res1 = client.Cypher.Match("(n:Book), (m:Writer), (k:Genre), (g:PublishingHouse)")
                                .Where("id(n)=" + bookID + " AND id(m)=" + writer.Id + " AND id(k)=" + idGenre + " AND id(g)="+houseId)
                                .Create("(m)-[r:WROTE]->(n)," + " (n)-[q:ASSIGN]->(k)," + " (g)-[l:PUBLISH]->(n)")
                                .ExecuteWithoutResultsAsync();
            }
            else
            {
                Writer newWriter = new Writer
                {
                    Firstname = firstname,
                    Lastname = lastname,
                    Birthplace = birthPlace,
                    BirthYear = birthYear,
                    YearOfDeath = yearOfDeath,
                    Biography = biography
                };

                var writerRes = client.Cypher.Create("(k:Writer $writer)")
                            .WithParam("writer", newWriter)
                            .With("k{.*, Id:id(k)} as u")
                            .Return(u => u.As<Writer>()).ResultsAsync.Result.FirstOrDefault();

                var writerId = writerRes.Id;

                var res1 = client.Cypher.Match("(n:Book), (m:Writer), (k:Genre), (g:PublishingHouse)")
                                .Where("id(n)=" + bookID + " AND id(m)=" + writerId + " AND id(k)=" + idGenre + " AND id(g)=" + houseId)
                                .Create("(m)-[r:WROTE]->(n)," + " (n)-[q:ASSIGN]->(k)," + " (g)-[l:PUBLISH]->(n)")
                                .ExecuteWithoutResultsAsync();
            }


            return bookID;
        }

        public int AddBookByPublishingHouseWithExistingWriter(int houseId, string title, string yearOfPublication, string description, int idGenre, string firstname, string lastname)
        {
            var genre = client.Cypher.Match("(n:Genre)")
                                    .Where("id(n)=" + idGenre)
                                    .Return(n => n.As<Genre>())
                                    .ResultsAsync.Result.FirstOrDefault();

            if (genre == null)
            {
                throw new InvalidOperationException("Genre does not exist!");
            }

            var writer = client.Cypher.Match("(w:Writer)")
                          .Where("w.Firstname='" + firstname + "' AND w.Lastname='" + lastname + "'")
                          .With("w{.*, Id:id(w)} as u")
                          .Return(u => u.As<Writer>()).ResultsAsync.Result.FirstOrDefault();

            if (writer == null)
            {
                throw new InvalidOperationException("Writer does not exist!");
            }


            var book = client.Cypher.Match("(n:Book), (k:PublishingHouse), (k)-[r:PUBLISH]->(n)")
                       .Where("n.Title=" + "'" + title + "'" + " AND id(k)=" + houseId)
                       .Return(n => n.As<Book>()).ResultsAsync.Result.FirstOrDefault();


            if (book != null)
            {
                throw new InvalidOperationException("Book already exist!");
            }

            Book newBook = new Book
            {
                Id = 0,
                Title = title,
                YearOfPublication = yearOfPublication,
                Description = description
            };


            var bookRes = client.Cypher.Create("(k:Book $book)")
                        .WithParam("book", newBook)
                        .With("k{.*, Id:id(k)} as u")
                        .Return(u => u.As<Book>()).ResultsAsync.Result;
            var book1 = bookRes.FirstOrDefault();
            var bookID = book1.Id;



            var res1 = client.Cypher.Match("(n:Book), (m:Writer), (k:Genre), (g:PublishingHouse)")
                            .Where("id(n)=" + bookID + " AND id(m)=" + writer.Id + " AND id(k)=" + idGenre + " AND id(g)=" + houseId)
                            .Create("(m)-[r:WROTE]->(n)," + " (n)-[q:ASSIGN]->(k)," + " (g)-[l:PUBLISH]->(n)")
                            .ExecuteWithoutResultsAsync();
            return bookID;

        }

        public void AddBook1(string nameBook, string year, string description)
        {
            Book newBook = new Book
            {
                Title = nameBook,
                YearOfPublication = year,
                Description = description
            };


            client.Cypher.Create("(k:Book $book)")
                                .WithParam("book", newBook)
                                .ExecuteWithoutResultsAsync();



        }

        public object CreateRelationship(int bookId, int genreId, int writerId)
        {
            var genres = client.Cypher.Match("(n:Genre)")
                                    .Where("id(n)=" + genreId)
                                    .Return(n => n.As<Genre>())
                                    .ResultsAsync.Result;

            var genre = genres.First();

            var writers = client.Cypher.Match("(n:Writer)")
                                    .Where("id(n)=" + writerId)
                                    .Return(n => n.As<Writer>())
                                    .ResultsAsync.Result;

            var writer = writers.First();

            var books = client.Cypher.Match("(n:Book)")
                                    .Where("id(n)=" + bookId)
                                    .Return(n => n.As<Book>())
                                    .ResultsAsync.Result;

            var book = books.First();


            var res1 = client.Cypher.Match("(n:Book), (m:Writer), (k:Genre)")
                                .Where("id(n)=" + bookId + " AND id(m)=" + writerId + " AND id(k)=" + genreId)
                                .Create("(m)-[r:WROTE]->(n)," + " (n)-[q:ASSIGN]->(k) ")
                                .Return((n, m, k) => new
                                {
                                    Book = n.As<Book>(),
                                    Writer = m.As<Writer>(),
                                    Genre = k.As<Genre>()
                                });
            return res1;

        }

        public List<Book> GetBooksByWriter(string firstname, string lastname, int userId)
        {
            var writer = client.Cypher.Match("(n:Writer)")
                                     .Where("n.Firstname='" + firstname +"' AND n.Lastname='" + lastname + "'")
                                     .Return(n => n.As<Writer>())
                                     .ResultsAsync.Result.FirstOrDefault();

            var user = client.Cypher.Match("(n:User)")
                                    .Where("id(n)=" + userId)
                                    .Return(n => n.As<User>())
                                    .ResultsAsync.Result.FirstOrDefault();


            if (writer == null || user == null)
            {
                throw new InvalidOperationException("Writer or user does not exist!");
            }

            var book = client.Cypher.Match("(n:Book), (m:Writer), (k:User), (m)-[r:WROTE]->(n), (k)-[q:READ]->(n)")
                                .Where("m.Firstname='" + firstname + "' AND m.Lastname='" + lastname + "'")
                                .Return((n) => n.As<Book>()).ResultsAsync.Result;

            return book.ToList();
        }

        public List<object> GetBooksByUser(int userId)
        {
            var user = client.Cypher.Match("(n:User)")
                                     .Where("id(n)=" + userId)
                                     .Return(n => n.As<User>())
                                     .ResultsAsync.Result.FirstOrDefault();

            if (user == null)
            {
                throw new InvalidOperationException("User does not exist!");
            }

            var books = client.Cypher.Match("(n:Book), (m:User), (w:Writer), (g:Genre), (n)-[r:ASSIGN]->(g), (w)-[q:WROTE]->(n), (m)-[p:READ]->(n)")
                                .Where("id(m)=" + userId)
                                .Return((n, w, g) => new { Book = n.As<Book>(), Writer = w.As<Writer>(), Genre = g.As<Genre>() })
                                .ResultsAsync.Result.ToList().Distinct();

            var listOfObject = new List<object>();

            foreach (var item in books)
            {
                var marks = client.Cypher.Match("(n:Book), (k:Mark), (n)-[q:RATED]->(k)")
                                .Where("n.Title=" + "'" + item.Book.Title + "'")
                                .Return((k) =>k.As<Mark>() )
                                .ResultsAsync.Result.ToList();
                float sum = 0;

                foreach (var itemInMarks in marks)
                {
                    sum += itemInMarks.Number;
                }

                if (sum != 0)
                {
                    sum = sum / (float)marks.Count;
                }

                listOfObject.Add(new { 
                    bookId = item.Book.Id,
                    title = item.Book.Title,
                    yearOfPublication = item.Book.YearOfPublication,
                    description = item.Book.Description,
                    writerId = item.Writer.Id,
                    firstName = item.Writer.Firstname,
                    lastName = item.Writer.Lastname,
                    genreId = item.Genre.Id,
                    genreName = item.Genre.Name,
                    Mark = sum });
            }

            return listOfObject.Distinct().ToList();
        }

        public object GetBookById(int bookId)
        {
            var book = client.Cypher.Match("(n:Book), (m:Genre), (n)-[r:ASSIGN]->(m)")
                                    .Where("id(n)=" + bookId)
                                    .Return((n,m) => new
                                    {
                                      Book = n.As<Book>(), 
                                      Genre = m.As<Genre>()
                                    }).ResultsAsync.Result.FirstOrDefault();
           
            return book;
        }

    }
}
