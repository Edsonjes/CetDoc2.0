using Microsoft.Extensions.Configuration;
namespace Infra.Contexto
{
	internal class DBConnect
	{
		public IConfiguration _Configuration;

		public DBConnect(IConfiguration Configuration)
		{
			_Configuration = Configuration;
		}

		public string GetConectionString()
		{
			return _Configuration.GetConnectionString("dbConnection");
		}
	}
}
