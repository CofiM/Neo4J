using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindBook.Core.Models
{
    public class Writer
    {
        [Key]
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Birthplace { get; set; }

        public string BirthYear { get; set; }

        public string YearOfDeath { get; set; }

        public string Biography { get; set; }

    }
}
