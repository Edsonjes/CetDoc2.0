using Microsoft.Extensions.Configuration;
namespace Infra.Contexto
{
	internal class ConnectionsDB
	{
		public IConfiguration _Configuration;

		public ConnectionsDB(IConfiguration Configuration)
		{
			_Configuration = Configuration;
		}

		public string GetConectionString()
		{
			return _Configuration.GetConnectionString("dbConnection");
		}
	}
}
