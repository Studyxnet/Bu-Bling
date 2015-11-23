using System;

using Xamarin.Forms;
using bubling;
using bubling.Droid;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(CustomWebView), typeof(WebViewCustom_Droid))]
namespace bubling.Droid
{
    public class WebViewCustom_Droid : WebViewRenderer
    {
        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (Control != null)
                Control.SetInitialScale(80);

            base.OnElementPropertyChanged(sender, e);
        }
    }
}

