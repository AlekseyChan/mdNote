using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(mdNote.UWP.ExitService))]
namespace mdNote.UWP
{
    public class ExitService: mdNote.Services.IExitService
    {
        public void Exit()
        {
            mdNote.UWP.App.Current.Exit();
        }
    }
}
