using AutoMapper;
using Dominio.Interfaces;
using Dominio.Model;
using Dominio.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Servicos;

namespace Infra.Repository
{
    public class AuthenticationRepository : IAuthentication
    {

		public IConfiguration _Configuration;
		public AuthenticationRepository(IConfiguration Configuration)
		{
			_Configuration = Configuration;
		}
		public readonly Authentication _authenticationServices;
        public readonly Mapper _mapper;
        public AuthenticationRepository(Authentication authenticationServices, Mapper mapper)
        {
            _authenticationServices = authenticationServices;
            _mapper = mapper;
        }

		

		public async Task<ActionResult<dynamic>> Login(UserViewModel obj)
        {
            try
            {
                var objMap = _mapper.Map<User>(obj);
                var user = await _authenticationServices.Login(objMap);
                if (user == null)
                    return new NotFoundObjectResult(new { message = "Usuário ou senha inválidos" });

                return new { user = user };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
