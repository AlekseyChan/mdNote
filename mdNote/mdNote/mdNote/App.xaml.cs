using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using mdNote.Controls;
using mdNote.Pages;
using System;
using System.Threading.Tasks;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace mdNote
{
	public partial class App : Application
	{
        public App()
		{
			InitializeComponent();

			SetMainPage();
		}

		public static void SetMainPage()
		{
//            Current.MainPage = new Pages.Page1();
            Current.MainPage = new Pages.MainPage();
        }
    }
}
