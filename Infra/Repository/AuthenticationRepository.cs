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
		public Authentication _authenticationServices;
		public Mapper _mapper;

		public AuthenticationRepository(IConfiguration Configuration)
		{
			_Configuration = Configuration;

			_authenticationServices = new Authentication(_Configuration);
			_mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<UserViewModel, User>().ReverseMap();
			})); ;
			
		}

		public AuthenticationRepository(Authentication authenticationServices, Mapper mapper)
		{
			_authenticationServices = authenticationServices;
			_mapper = mapper;
		}

		public async Task<ActionResult<dynamic>> Login(UserViewModel obj)
		{
			try
			{
			    var UserMap = _mapper.Map<User>(obj);
				var user = await _authenticationServices.Login(UserMap);

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
