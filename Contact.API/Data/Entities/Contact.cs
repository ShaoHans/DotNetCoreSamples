using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.API.Data.Entities
{
    /// <summary>
    /// 好友
    /// </summary>
    public class Contact
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Company { get; set; }

        public string Position { get; set; }

        public string Avatar { get; set; }
    }
}
