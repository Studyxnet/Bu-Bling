using System;
using System.Net;
using System.IO;

using Xamarin.Forms;
using bubling;
using bubling.Droid;

[assembly: Dependency(typeof(GetDOM_Droid))]
namespace bubling.Droid
{
    public class GetDOM_Droid : IGetDOM
    {
        public string ResolveDOM()
        {
            using (var client = new WebClient())
            {
                if (String.IsNullOrEmpty(App.EMAIL_USUARIO))
                    return String.Empty;
                
                var data = client
                    .DownloadString(String.Format("http://cpro21201.publiccloud.com.br/bu-bling/mobile/carga_campanhas.aspx?email={0}", App.EMAIL_USUARIO));

                using (var stream = new StringReader(data))
                {
                    var _DOM = stream.ReadToEnd();

                    return _DOM;
                }
            }
        }
    }
}

