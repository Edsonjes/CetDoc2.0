using AutoMapper;
using Dominio.Model;
using Dominio.ViewModel;


namespace Dominio.Profiles
{
	public class MapperConfig 
	{
		public static MapperConfiguration RegisterMap()
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<CurriculoProfissionalModel, CurriculoProfissionalViewModel>().ReverseMap();
				cfg.CreateMap<Pessoa, PessoaViewModel>().ReverseMap();
			});
			return config;
		}
		
	}
}
