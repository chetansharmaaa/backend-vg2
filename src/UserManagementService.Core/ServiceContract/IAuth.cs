using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementService.Core.Models;

namespace UserManagementService.Core.ServiceContract
{
    public interface IAuth
    {
        string GenerateToken(User user);
    }
}
