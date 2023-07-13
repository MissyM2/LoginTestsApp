using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginTestsApp.Console
{
    public interface IUserService
    {
        User LoginUser(string email);
    }
}
