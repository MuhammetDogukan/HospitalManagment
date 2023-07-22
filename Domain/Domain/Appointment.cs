using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Appointment : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        [Required]
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        [Required]
        public DateTime AppointmentDate { get; set; }
        public bool IsFamilyDoctor { get; set; }
        public bool IsApproved { get; set; }
        public virtual Doctor Doctor { get; set; }        
        public virtual Patients Patient { get; set; }
        public ICollection<Medication> Medications { get; set; }
    }
}
