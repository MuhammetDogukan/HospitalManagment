using Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain
{
    public class DoctorWithToken
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }

    }
}
