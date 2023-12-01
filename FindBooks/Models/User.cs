using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindBook.Core.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Mail { get; set; }

        public string Password { get; set; }

        public char Role { get; set; }


    }
}
