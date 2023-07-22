using Core.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain
{
    public class User : IdentityUser
    {
        [ForeignKey("Doctor")]
        public new int Id { get; set; }
        public override string UserName { get; set; }
        public string Password { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}
