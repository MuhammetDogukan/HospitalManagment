using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class PatientsRes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FamilyDoctorId { get; set; }
        public virtual ICollection<Doctor> FamilyDoctors { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
