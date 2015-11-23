using System;

using Xamarin.Forms;

namespace bubling
{
    public class Home : ContentPage
    {
        private CustomWebView _webView;
        private StackLayout _mainLayout;

        public Home(string url)
        {
            this.Title = "Home";

            if (!String.IsNullOrEmpty(url))
            {
                if (this.UsuarioLogado())
                    this.PopulaEmailUsuario();

                this._webView = new CustomWebView
                { 
                    Source = !this.UsuarioLogado() ? url : String.Concat(App.URL_USUARIO_LOGADO, App.EMAIL_USUARIO), 
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

        private void PopulaEmailUsuario()
        {
            var repo = new BuBlingRepository();
            App.EMAIL_USUARIO = repo.RetornarEmailUsuarioLogado();
        }

        private bool UsuarioLogado()
        {
            var repo = new BuBlingRepository();
            return repo.UsuarioLogado();
        }
    }
}


