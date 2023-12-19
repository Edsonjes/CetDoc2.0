using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Models.Context
{
	public class DBConections : IdentityDbContext<ApplicationUser>
	{
		
		public DBConections(DbContextOptions<DBConections> options) : base(options)
		{
		}
		
	}
}
