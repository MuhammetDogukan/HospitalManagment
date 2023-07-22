using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class DoctorRes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }
        public virtual ICollection<Appointment> Appointments { get; init; }

    }
}
