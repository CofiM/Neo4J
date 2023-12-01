using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindBook.Core.Models
{
    public class Mark
    {
        [Key]
        public int Id { get; set; }

        public float Number { get; set; }


    }
}
