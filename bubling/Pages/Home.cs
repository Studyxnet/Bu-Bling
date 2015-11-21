using System;

using Xamarin.Forms;

namespace bubling
{
    public class Home : ContentPage
    {
        private WebView _webView;
        private StackLayout _mainLayout;

        public Home(string url)
        {
            this.Title = "Home";

            if (!String.IsNullOrEmpty(url))
            {
                this._webView = new WebView
                { 
                    Source = url, 
                    HeightRequest = 1000,
                    WidthRequest = 1000
                };

                this._mainLayout = new StackLayout
                {
                    Children = { _webView }
                };

                this.Content = _mainLayout;
            }
            else
                DisplayAlert("Aviso", "Erro ao carregar conteúdo", "OK");
        }
    }
}


