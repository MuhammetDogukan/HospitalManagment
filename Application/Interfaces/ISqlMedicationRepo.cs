using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISqlMedicationRepo
    {
        Task<Medication> GetMedicationById(int medicationId);
        Task<IEnumerable<Medication>> GetAllMedications();
        Task UpdateMedication(Medication medication);
        Task DeleteMedication(int medicationId);
    }
}
