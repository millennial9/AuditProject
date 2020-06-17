using System;
using System.Collections.Generic;
using System.Text;

namespace AuditProject.Models
{
    public class Case
    {
        public string IdCase { get; set; }
        public string DescCase { get; set; }
        public UserInfo Requester { get; set; }

    }
}
