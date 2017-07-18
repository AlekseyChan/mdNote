using Xamarin.Forms;

[assembly: Dependency(typeof(mdNote.UWP.BaseUrl))]
namespace mdNote.UWP
{
    class BaseUrl: mdNote.Services.IBaseUrl
    {
        public string Get()
        {
            return "ms-appx-web:///";
        }
    }
}
