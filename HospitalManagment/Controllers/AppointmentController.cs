using Application.Interfaces;
using Core.Aplication;
using Core.Domain;
using Domain.DomainRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DoctorAppointmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly ISqlAppointmentRepo _appointmentRepository;
        private readonly ISqlDoctorRepo _doctorRepository;
        private readonly ISqlPatientsRepo _patientsRepository;

        public AppointmentController(ISqlAppointmentRepo appointmentRepository, ISqlDoctorRepo doctorRepository, ISqlPatientsRepo patientsRepository)
        {
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
            _patientsRepository = patientsRepository;   
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentRes>>> GetAllAppointments()
        {
            var appointments = await _appointmentRepository.GetAllAppointments();

            IEnumerable<AppointmentRes> appointmentsRes = new List<AppointmentRes>();
            foreach (var appointment in appointments)
            {
                AppointmentRes appointmentRes = new()
                {
                    Id = appointment.Id,
                    Name = appointment.Name,
                    DoctorId = appointment.DoctorId,
                    PatientId = appointment.PatientId,
                    AppointmentDate = appointment.AppointmentDate,
                    IsFamilyDoctor = appointment.IsFamilyDoctor,
                    IsApproved = appointment.IsApproved,
                    Medications = appointment.Medications
                };
                appointmentsRes.Append(appointmentRes);
            }

            return Ok(appointmentsRes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentRes>> GetAppointmentById(int id)
        {
            var appointment = await _appointmentRepository.GetAppointmentById(id);
            if (appointment == null)
            {
                return NotFound();
            }
            AppointmentRes appointmentRes = new()
            {
                Id = appointment.Id,
                Name = appointment.Name,
                DoctorId = appointment.DoctorId,
                PatientId = appointment.PatientId,
                AppointmentDate = appointment.AppointmentDate,
                IsFamilyDoctor = appointment.IsFamilyDoctor,
                IsApproved = appointment.IsApproved,
                Medications = appointment.Medications
            };

            return Ok(appointment);
        }

        [HttpGet("Doctor/{doctorId}"),Authorize]
        public async Task<ActionResult<IEnumerable<AppointmentRes>>> GetAppointmentsByDoctorId(int doctorId)
        {
            var appointments = await _appointmentRepository.GetAppointmentsByDoctorId(doctorId);

            IEnumerable<AppointmentRes> appointmentsRes = new List<AppointmentRes>();
            foreach (var appointment in appointments)
            {
                AppointmentRes appointmentRes = new()
                {
                    Id = appointment.Id,
                    Name = appointment.Name,
                    DoctorId = appointment.DoctorId,
                    PatientId = appointment.PatientId,
                    AppointmentDate = appointment.AppointmentDate,
                    IsFamilyDoctor = appointment.IsFamilyDoctor,
                    IsApproved = appointment.IsApproved,
                    Medications = appointment.Medications
                };
                appointmentsRes.Append(appointmentRes);
            }

            return Ok(appointmentsRes);
        }

        [HttpGet("Patient/{patientId}")]
        public async Task<ActionResult<IEnumerable<AppointmentRes>>> GetAppointmentsByPatientId(int patientId)
        {
            var appointments = await _appointmentRepository.GetAppointmentsByPatientId(patientId);

            IEnumerable<AppointmentRes> appointmentsRes = new List<AppointmentRes>();
            foreach (var appointment in appointments)
            {
                AppointmentRes appointmentRes = new()
                {
                    Id = appointment.Id,
                    Name = appointment.Name,
                    DoctorId = appointment.DoctorId,
                    PatientId = appointment.PatientId,
                    AppointmentDate = appointment.AppointmentDate,
                    IsFamilyDoctor = appointment.IsFamilyDoctor,
                    IsApproved = appointment.IsApproved,
                    Medications = appointment.Medications
                };
                appointmentsRes.Append(appointmentRes);
            }

            return Ok(appointmentsRes);
        }

        [HttpPost]
        public async Task<ActionResult<AppointmentRes>> CreateAppointment(AppointmentReq appointmentReq)
        {
           

            var Doctor = await _doctorRepository.GetDoctorById(appointmentReq.DoctorId);
            if (Doctor == null)
                return NotFound("The specified dcotor couldn't be found.");
            var Patients = await _patientsRepository.GetPatientById(appointmentReq.PatientId);
            if (Patients == null)
                return NotFound("The specified patient couldn't be found.");

            Appointment appointment = new()
            {
                Name = appointmentReq.Name,
                DoctorId = appointmentReq.DoctorId,
                PatientId = appointmentReq.PatientId,
                AppointmentDate = appointmentReq.AppointmentDate,
                IsFamilyDoctor = false,
                IsApproved = false
            };

            if (Patients.FamilyDoctorId == Doctor.Id)
            {
                appointment.IsFamilyDoctor = true;
            }

            AppointmentRes appointmentRes = new()
            {
                Id = appointment.Id,
                Name = appointment.Name,
                DoctorId = appointment.DoctorId,
                PatientId = appointment.PatientId,
                AppointmentDate = appointment.AppointmentDate,
                IsFamilyDoctor = appointment.IsFamilyDoctor,
                IsApproved = appointment.IsApproved,
                Medications = appointment.Medications
            };


            await _appointmentRepository.AddAppointment(appointment);
            return Ok(appointmentRes);
        }

        [HttpPut(Name = "Approve appointment and enter medication"), Authorize]
        public async Task<ActionResult<AppointmentRes>> ApproveAppointment(int id, AppointmentApprove AppointmentApprove)
        {
            var appointment = await _appointmentRepository.GetAppointmentById(id);
            if (appointment == null)
            {
                return NotFound();
            }

            appointment.IsApproved = AppointmentApprove.IsApproved;
            appointment.Medications = AppointmentApprove.Medications;

            await _appointmentRepository.UpdateAppointment(appointment);

            AppointmentRes appointmentRes = new()
            {
                Id = appointment.Id,
                Name = appointment.Name,
                DoctorId = appointment.DoctorId,
                PatientId = appointment.PatientId,
                AppointmentDate = appointment.AppointmentDate,
                IsFamilyDoctor = appointment.IsFamilyDoctor,
                IsApproved = appointment.IsApproved,
                Medications = appointment.Medications
            };

            return Ok(appointmentRes);
        }

        [HttpPut("{id}"), Authorize]
        public async Task<ActionResult<AppointmentRes>> UpdateAppointment(int id, AppointmentReq updatedAppointment)
        {
            var appointment = await _appointmentRepository.GetAppointmentById(id);
            if (appointment == null)
            {
                return NotFound();
            }


            var Doctor = await _doctorRepository.GetDoctorById(updatedAppointment.DoctorId);
            if (Doctor == null)
                return NotFound("The specified dcotor couldn't be found.");
            var Patients = await _patientsRepository.GetPatientById(updatedAppointment.PatientId);
            if (Patients == null)
                return NotFound("The specified patient couldn't be found.");
            if (Patients.FamilyDoctorId == Doctor.Id)
            {
                appointment.IsFamilyDoctor = true;
            }
            appointment.IsFamilyDoctor = false;


            appointment.Name = updatedAppointment.Name;
            appointment.DoctorId = updatedAppointment.DoctorId;
            appointment.PatientId = updatedAppointment.PatientId;
            appointment.AppointmentDate = updatedAppointment.AppointmentDate;

            await _appointmentRepository.UpdateAppointment(appointment);

            AppointmentRes appointmentRes = new()
            {
                Id = appointment.Id,
                Name = appointment.Name,
                DoctorId = appointment.DoctorId,
                PatientId = appointment.PatientId,
                AppointmentDate = appointment.AppointmentDate,
                IsFamilyDoctor = appointment.IsFamilyDoctor,
                IsApproved = appointment.IsApproved,
                Medications = appointment.Medications
            };

            return Ok(appointmentRes);
        }

        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult> DeleteAppointment(int id)
        {
            var appointment = await _appointmentRepository.GetAppointmentById(id);
            if (appointment == null)
            {
                return NotFound();
            }



            await _appointmentRepository.DeleteAppointment(id);
            return NoContent();
        }
    }
}
