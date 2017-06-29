using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mdNote.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(AdvWebView), typeof(mdNote.UWP.AdvWebViewRender))]
namespace mdNote.UWP
{
    public class AdvWebViewRender:WebViewRenderer
    {
        protected async override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);
            var webView = e.NewElement as AdvWebView;
            if (webView != null)
                webView.EvaluateJavascript = async (js) =>
                {
                    string res = await Control.InvokeScriptAsync("eval", new[] { js });
                    return res;
                };
        }
    }
}