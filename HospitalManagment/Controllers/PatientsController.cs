using Application.Interfaces;
using Core.Aplication;
using Core.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly ISqlPatientsRepo _patientRepository;
        private readonly ISqlDoctorRepo _doctorRepository;

        public PatientsController(ISqlPatientsRepo patientRepository, ISqlDoctorRepo doctorRepository)
        {
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientsRes>> GetPatientById(int id)
        {
            var patient = await _patientRepository.GetPatientById(id);
            if (patient == null)
            {
                return NotFound();
            }
            PatientsRes patientRes = new()
            {
                Id = patient.Id,
                Name = patient.Name,
                FamilyDoctorId = patient.FamilyDoctorId,
                FamilyDoctors = patient.FamilyDoctors,
                Appointments = patient.Appointments
            };

            return Ok(patient);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientsRes>>> GetAllPatients()
        {
            var patients = await _patientRepository.GetAllPatients();

            IEnumerable<PatientsRes> patientsRes = new List<PatientsRes>();
            foreach (var patient in patients)
            {
                PatientsRes patientRes = new()
                {
                    Id = patient.Id,
                    Name = patient.Name,
                    FamilyDoctorId = patient.FamilyDoctorId,
                    FamilyDoctors = patient.FamilyDoctors,
                    Appointments = patient.Appointments
                };
                patientsRes.Append(patientRes);
            }

            return Ok(patientsRes);
        }

        [HttpPost]
        public async Task<ActionResult<PatientsRes>> CreatePatient(PatientsReq patientReq)
        {
            Patients patient = new()
            {
                Name = patientReq.Name,
                FamilyDoctorId = patientReq.FamilyDoctorId
            };
            await _patientRepository.AddPatient(patient);

            PatientsRes patientRes = new()
            {
                Id = patient.Id,
                Name = patient.Name,
                FamilyDoctorId = patient.FamilyDoctorId,
                FamilyDoctors = patient.FamilyDoctors,
                Appointments = patient.Appointments
            };

            return Ok(patientRes);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PatientsRes>> UpdatePatient(int id, PatientsReq updatedPatient)
        {
            var patient = await _patientRepository.GetPatientById(id);
            if (patient == null)
            {
                return NotFound();
            }
            if (updatedPatient.FamilyDoctorId != patient.FamilyDoctorId)
            {
                patient.FamilyDoctors.Add(await _doctorRepository.GetDoctorById(patient.FamilyDoctorId));
            }

            patient.Name = updatedPatient.Name;
            patient.FamilyDoctorId = updatedPatient.FamilyDoctorId;

            await _patientRepository.UpdatePatient(patient);

            PatientsRes patientRes = new()
            {
                Id = patient.Id,
                Name = patient.Name,
                FamilyDoctorId = patient.FamilyDoctorId,
                FamilyDoctors = patient.FamilyDoctors,
                Appointments = patient.Appointments
            };

            return Ok(patientRes);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePatient(int id)
        {
            var patient = await _patientRepository.GetPatientById(id);
            if (patient == null)
            {
                return NotFound();
            }

            await _patientRepository.DeletePatient(id);
            return NoContent();
        }
    }
}
