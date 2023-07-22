using Application.Interfaces;
using Core.Aplication;
using Core.Domain;
using Domain.Domain;
using Domain.DomainRequest;
using HospitalManagment;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Application.Aplication
{
    public class SqlUserRepo : ISqlUserRepo
    {
        private readonly IHospitalContext _context;

        public SqlUserRepo(IHospitalContext context)
        {
            _context = context;
        }

        public async Task RegisterAsync(RegistrationReq model)
        {
            // Check if the email is already registered
            if (await _context.Users.AnyAsync(u => u.UserName == model.UserName))
            {
                throw new ArgumentException("Email is already registered.");
            }

            if (model.Password!=model.AgainPassword)
            {
                throw new ArgumentException("Passwor and password again are not equal.");
            }
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == model.Id);
            if (doctor == null)
            {
                throw new ArgumentException("There isn't any doctor that you entered.");
            }


            // Create a new user
            var user = new User
            {
                Id=model.Id,
                UserName = model.UserName,
                Password = model.Password,

                // Fill in other user properties if needed
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<DoctorWithToken> LoginAsync(LoginReq model)
        {
            // Find the user by email
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == model.UserName);

            if (user == null)
            {
                throw new ArgumentException("Invalid email or password.");
            }


            if (user.Password != model.Password)
            {
                throw new ArgumentException("Invalid email or password.");
            }
            TokenService tokenService = new TokenService();

            var accessToken = tokenService.CreateToken(user);
            
            DoctorWithToken doctorWithToken = new()
            {
                Id = user.Id,
                Name = user.UserName,
                Token = accessToken
            };


            return doctorWithToken;
        }

    }
}
