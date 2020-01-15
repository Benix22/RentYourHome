using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentYourHomeAPI.Models
{
    public class Home
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public int Stars { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Imagen { get; set; }

        public Owner Owner { get; set; }

        public Home() { }
    }
}
