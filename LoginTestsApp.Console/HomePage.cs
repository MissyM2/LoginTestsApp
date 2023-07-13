using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginTestsApp.Console
{
    public class HomePage : Page
    {
        public HomePage(User user)
        {
            User = user;
            
        }
        public User User { get; }
    }
}
