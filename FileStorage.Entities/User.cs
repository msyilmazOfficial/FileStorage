using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Entities
{
    public class User : Base
    {
        public string? Email { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
    }
}
