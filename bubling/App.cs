using System;

using Xamarin.Forms;

namespace bubling
{
    public class App : Application
    {
        public static string URL = "http://cpro21201.publiccloud.com.br/bu-bling/mobile/login.aspx";
        public static string URL_USUARIO_LOGADO = "http://cpro21201.publiccloud.com.br/bu-bling/mobile/carga_campanhas.aspx?email=";
        public static string EMAIL_USUARIO = "";

        public App()
        {
            MainPage = new Home(URL);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

