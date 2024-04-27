using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woody.Models
{
    public class User
    {
        string Username { get; set; }
        string Password { get; set; } //password for now, removed later
        public List<Container> Containers { get; set; }
    }
}
