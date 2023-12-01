using FindBook.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindBooks.Models
{
    public class PublishingHouse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string YearOfEstablishment { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Contact { get; set; }

        public string Place { get; set; }

        public char Role { get; set; }
    }
}
