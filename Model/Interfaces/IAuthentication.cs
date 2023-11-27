using Dominio.Model;
using Dominio.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IAuthentication
    {
        public Task<string> Login(UserViewModel obj); 
    }
}
