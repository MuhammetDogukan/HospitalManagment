using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISqlDoctorRepo
    {
        public Task<Doctor> GetDoctorById(int doctorId);
        public Task<IEnumerable<Doctor>> GetAllDoctors();
        public Task AddDoctor(Doctor doctor);
        public Task UpdateDoctor(Doctor doctor);
        public Task DeleteDoctor(int doctorId);
    }
}
