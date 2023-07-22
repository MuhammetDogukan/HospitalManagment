using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISqlAppointmentRepo
    {
        public Task<Appointment> GetAppointmentById(int appointmentId);
        public Task<IEnumerable<Appointment>> GetAllAppointments();
        public Task<IEnumerable<Appointment>> GetAppointmentsByDoctorId(int doctorId);
        public Task<IEnumerable<Appointment>> GetAppointmentsByPatientId(int patientId);
        public Task AddAppointment(Appointment appointment);
        public Task UpdateAppointment(Appointment appointment);
        public Task DeleteAppointment(int appointmentId);

    }
}
