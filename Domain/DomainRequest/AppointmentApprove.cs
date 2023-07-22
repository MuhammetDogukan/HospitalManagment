using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainRequest
{
    public class AppointmentApprove
    {
        public bool IsApproved { get; set; }
        public ICollection<Medication> Medications { get; set; }
    }
}
