using AutoMapper;
using Dominio.Model;
using Dominio.ViewModel;


namespace Dominio.Profiles
{
	internal class Mapper : Profile
	{
		public Mapper()
		{
			var config = new MapperConfiguration(cfg => {
							cfg.CreateMap<PessoaViewModel, Pessoa>().ReverseMap();
				            cfg.CreateMap<User, UserViewModel>().ReverseMap();
			});

		}
	}
}
