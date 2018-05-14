using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Member.API.Data.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Company { get; set; }

        public string Position { get; set; }
    }
}
