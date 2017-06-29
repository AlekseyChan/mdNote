using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace mdNote.Controls
{
    public class AdvWebView: WebView
    {
        public static BindableProperty EvaluateJavascriptProperty =BindableProperty.Create(nameof(EvaluateJavascript), typeof(Func<string, Task<string>>), typeof(AdvWebView), null, BindingMode.OneWayToSource);

        public Func<string, Task<string>> EvaluateJavascript
        {
            get { return (Func<string, Task<string>>)GetValue(EvaluateJavascriptProperty); }
            set { SetValue(EvaluateJavascriptProperty, value); }
        }

        public async Task<string> TryEval(string exp)
        {
            string result = String.Empty;
            try
            {
                result = await EvaluateJavascript(exp);
            }
            catch (Exception e)
            {
                result = e.Message;
                System.Diagnostics.Debug.WriteLine(result);
            }
            return result;
        }
    }
}
