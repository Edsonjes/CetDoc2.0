using AutoMapper;
using Dominio.Model;
using Dominio.ViewModel;


namespace Dominio.Profiles
{
	public class Mapper : Profile
	{

		public Mapper()
		{
			ConfigureMappings();
		}

		private void ConfigureMappings()
		{
			var config = new MapperConfiguration(cfg => {
				cfg.CreateMap<PessoaViewModel, Pessoa>().ReverseMap();
				cfg.CreateMap<User, UserViewModel>().ReverseMap();
			});

		}
	}
}
