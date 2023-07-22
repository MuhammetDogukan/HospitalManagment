using Core.Domain;
using Domain.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aplication
{
    public interface IHospitalContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Medication> Medication { get; set; }
        public DbSet<Patients> Patients { get; set; }
        public DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync();
    }
}
