using Microsoft.AspNetCore.Identity;

namespace IdentityService.Models.Context
{
	public class AplicationUser : IdentityUser
	{
          private string FirstName { get; set; }
		  private string LastName { get; set; }	
	}
}
