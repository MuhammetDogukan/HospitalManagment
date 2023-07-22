using Application.Interfaces;
using Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aplication
{
    public class SqlPatientsRepo : ISqlPatientsRepo
    {
        private readonly IHospitalContext _context;

        public SqlPatientsRepo(IHospitalContext context)
        {
            _context = context;
        }

        public async Task<Patients> GetPatientById(int patientId)
        {
            return await _context.Patients
                .Include(p => p.FamilyDoctorId)
                .FirstOrDefaultAsync(p => p.Id == patientId);
        }

        public async Task<IEnumerable<Patients>> GetAllPatients()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task AddPatient(Patients patient)
        {
            patient.FamilyDoctors.Add(await _context.Doctors.FirstOrDefaultAsync(p => p.Id == patient.FamilyDoctorId));
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePatient(Patients patient)
        {

            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePatient(int patientId)
        {
            var patient = await _context.Patients.FindAsync(patientId);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }
        }
    }
}
