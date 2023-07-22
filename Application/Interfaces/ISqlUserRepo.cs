using Domain.Domain;
using Domain.DomainRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISqlUserRepo
    {
        public Task RegisterAsync(RegistrationReq model);
        public Task<DoctorWithToken> LoginAsync(LoginReq model);
    }
}
