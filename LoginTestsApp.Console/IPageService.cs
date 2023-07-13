using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginTestsApp.Console
{
    public interface IPageService
    {
        public Task PushAsync(Page page);
        Task DisplayAlert(string header, string message, string button);
    }
}
