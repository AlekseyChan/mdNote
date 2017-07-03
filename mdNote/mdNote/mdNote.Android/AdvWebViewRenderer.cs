using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mdNote.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

[assembly: ExportRenderer(typeof(AdvWebView), typeof(mdNote.Droid.AdvWebViewRenderer))]
namespace mdNote.Droid
{
    public class AdvWebViewRenderer: WebViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);

            var webView = e.NewElement as AdvWebView;
            if (webView != null)
                webView.EvaluateJavascript = async (js) =>
                {
                    var reset = new System.Threading.ManualResetEvent(false);
                    var response = string.Empty;
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Control?.EvaluateJavascript(js, new JavascriptCallback((r) => { response = r; reset.Set(); }));
                    });
                    await Task.Run(() => { reset.WaitOne(); });
                    return response;
                };
        }
    }

    internal class JavascriptCallback : Java.Lang.Object, Android.Webkit.IValueCallback
    {
        public JavascriptCallback(Action<string> callback)
        {
            _callback = callback;
        }

        private Action<string> _callback;
        public void OnReceiveValue(Java.Lang.Object value)
        {
            Java.Lang.String strValue = (Java.Lang.String)value;
            string result = new System.String(strValue.ToCharArray());
            result = System.Text.RegularExpressions.Regex.Unescape(result);
            if (result.Length >= 2)
                result = result.Substring(1, result.Length - 2);
            _callback?.Invoke(result);
        }
    }
}