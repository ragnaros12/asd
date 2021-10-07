using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_proekt.Models;

namespace Web_rekuperator.Models
{
    public class Users
    {
        public int Id { get; set; }

        public List<Model> models { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
