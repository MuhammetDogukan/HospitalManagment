using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISqlPatientsRepo
    {
        public Task<Patients> GetPatientById(int patientId);
        public Task<IEnumerable<Patients>> GetAllPatients();
        public Task AddPatient(Patients patient);
        public Task UpdatePatient(Patients patient);
        public Task DeletePatient(int patientId);
    }
}
