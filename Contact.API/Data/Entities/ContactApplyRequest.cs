using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.API.Data.Entities
{
    /// <summary>
    /// 申请添加好友
    /// </summary>
    public class ContactApplyRequest
    {
        /// <summary>
        /// 被申请人Id
        /// </summary>
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Company { get; set; }

        public string Position { get; set; }

        public string Avatar { get; set; }

        /// <summary>
        /// 申请人Id
        /// </summary>
        public int ApplierId { get; set; }

        /// <summary>
        /// 0：未同意 1：已通过
        /// </summary>
        public string Approval { get; set; }

        public DateTime HandleTime { get; set; }
    }
}
