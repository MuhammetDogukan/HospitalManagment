
using Application.Interfaces;
using Core.Aplication;
using Core.Domain;
using Domain.Domain;
using HospitalManagment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DoctorAppointmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly ISqlDoctorRepo _doctorRepository;

        public DoctorController(ISqlDoctorRepo doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorRes>>> GetAllDoctors()
        {
            var doctors = await _doctorRepository.GetAllDoctors();
            IEnumerable<DoctorRes> doctorsRes = new List<DoctorRes>();
            foreach (var doctor in doctors)
            {
                DoctorRes doctorRes = new()
                {
                    Id = doctor.Id,
                    Name = doctor.Name,
                    Specialty = doctor.Specialty,
                    Appointments = doctor.Appointments
                };
                doctorsRes.Append(doctorRes);
            }
            return Ok(doctorsRes);
        }

        [HttpPost]
        public async Task<ActionResult<DoctorRes>> CreateDoctor(DoctorReq doctorReq)
        {
            Doctor doctor = new()
            {
                Name = doctorReq.Name,
                Specialty=doctorReq.Specialty
            };
            await _doctorRepository.AddDoctor(doctor);
            DoctorRes doctorRes = new()
            {
                Id=doctor.Id,
                Name=doctor.Name,
                Specialty=doctor.Specialty,
                Appointments=doctor.Appointments
            };
            return Ok(doctorRes);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DoctorRes>> UpdateDoctor(int id, DoctorReq updatedDoctor)
        {
            var doctor = await _doctorRepository.GetDoctorById(id);
            if (doctor == null)
            {
                return NotFound();
            }


            doctor.Name = updatedDoctor.Name;
            doctor.Specialty = updatedDoctor.Specialty;

            await _doctorRepository.UpdateDoctor(doctor);
            DoctorRes doctorRes = new()
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Specialty = doctor.Specialty,
                Appointments = doctor.Appointments
            };
            return Ok(doctorRes);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDoctor(int id)
        {
            var doctor = await _doctorRepository.GetDoctorById(id);
            if (doctor == null)
            {
                return NotFound();
            }

            await _doctorRepository.DeleteDoctor(id);
            return NoContent();
        }
    }
}
