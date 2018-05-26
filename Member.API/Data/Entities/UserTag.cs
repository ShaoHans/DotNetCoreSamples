using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Member.API.Data.Entities
{
    public class UserTag
    {
        public int TagId { get; set; }

        public int UserId { get; set; }

        public string Tag { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
