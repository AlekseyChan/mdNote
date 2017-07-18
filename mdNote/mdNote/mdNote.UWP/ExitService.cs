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
