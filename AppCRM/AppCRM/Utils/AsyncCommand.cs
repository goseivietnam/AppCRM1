using System;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace AppCRM.Utils
{
    public class AsyncCommand : Command
    {
        public AsyncCommand(Func<Task> execute) : base(() => execute())
        {
        }
        public AsyncCommand(Func<object, Task> execute) : base((parameters) => execute(parameters))
        {
        }
    }
}
