using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mdNote.Services;
using Xamarin.Forms;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

[assembly: Dependency(typeof(mdNote.Droid.BaseUrl))]
namespace mdNote.Droid
{
    public class BaseUrl: IBaseUrl
    {
        public string Get()
        {
            return "file:///android_asset/";
        }
    }
}