using Application.Interfaces;
using Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationController : ControllerBase
    {
        private readonly ISqlMedicationRepo _medicationRepository;

        public MedicationController(ISqlMedicationRepo medicationRepository)
        {
            _medicationRepository = medicationRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Medication>> GetMedicationById(int id)
        {
            var medication = await _medicationRepository.GetMedicationById(id);
            if (medication == null)
            {
                return NotFound();
            }

            return Ok(medication);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medication>>> GetAllMedications()
        {
            var medications = await _medicationRepository.GetAllMedications();
            return Ok(medications);
        }


        [HttpPut("{id}"),Authorize]
        public async Task<ActionResult<Medication>> UpdateMedication(int id, Medication updatedMedication)
        {
            var medication = await _medicationRepository.GetMedicationById(id);
            if (medication == null)
            {
                return NotFound();
            }

            medication.Name = updatedMedication.Name;

            await _medicationRepository.UpdateMedication(medication);
            return Ok(medication);
        }

        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult> DeleteMedication(int id)
        {
            var medication = await _medicationRepository.GetMedicationById(id);
            if (medication == null)
            {
                return NotFound();
            }

            await _medicationRepository.DeleteMedication(id);
            return NoContent();
        }
    }
}
