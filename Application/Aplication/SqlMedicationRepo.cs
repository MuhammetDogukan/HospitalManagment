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
    public class SqlMedicationRepo : ISqlMedicationRepo
    {
        private readonly IHospitalContext _context;

        public SqlMedicationRepo(IHospitalContext context)
        {
            _context = context;
        }

        public async Task<Medication> GetMedicationById(int medicationId)
        {
            return await _context.Medication.FindAsync(medicationId);
        }

        public async Task<IEnumerable<Medication>> GetAllMedications()
        {
            return await _context.Medication.ToListAsync();
        }


        public async Task UpdateMedication(Medication medication)
        {
            _context.Medication.Update(medication);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMedication(int medicationId)
        {
            var medication = await _context.Medication.FindAsync(medicationId);
            if (medication != null)
            {
                _context.Medication.Remove(medication);
                await _context.SaveChangesAsync();
            }
        }
    }
}
