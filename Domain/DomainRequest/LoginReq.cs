using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainRequest
{
    public class LoginReq
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
