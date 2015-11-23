using System;
using Android.Widget;
using Android.App;
using Android.Util;
using Android.Support.V4.App;
using Android.Content;

using Xamarin.Forms;
using TaskStackBuilder = Android.Support.V4.App.TaskStackBuilder;
using System.Xml.Serialization;
using System.IO;
using Android.Graphics;

namespace bubling.Droid
{
    [Service]
    public class PromoServices : Service
    {
        System.Threading.Timer _timer;

        public override StartCommandResult OnStartCommand(Android.Content.Intent intent, StartCommandFlags flags, int startId)
        {
            Log.Debug("PromoService", "PromoService iniciado");
            CallWebService();

            return base.OnStartCommand(intent, flags, startId);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            _timer.Dispose();
            Log.Debug("PromoService", "PromoService parado");       
        }

        private XMLString XMLDeserialization(string xml)
        {
            XmlSerializer x = new XmlSerializer(typeof(XMLString));
            var dados = (XMLString)x.Deserialize(new StringReader(xml));

            return dados;
        }

        public void CallWebService()
        {
            _timer = new System.Threading.Timer(async (o) =>
                {
                    var dadosAPI = await CallAPI_Droid.ExecChamadaAPIComRetornoString(EnumCallAPI.Promo);

                    if (String.IsNullOrEmpty(dadosAPI))
                        return;

                    // Configurando URL recebida pelo serviço
                    var xmlString = this.XMLDeserialization(dadosAPI);

                    App.URL = xmlString.Text.Split(',')[0];

                    // Salvando imagem e carregando o Bitmap
                    var saveImage = new SaveAndLoadFile_Droid();
                    var imageName = String.Concat(System.IO.Path.GetRandomFileName().Split('.')[0], ".jpg");
                    Bitmap bmpImagem = null;

                    if (saveImage.BaixaImagemSalvarEmDisco(imageName, xmlString.Text.Split(',')[1]))
                        bmpImagem = BitmapFactory.DecodeFile(saveImage.GetImage(imageName));

                    // Construindo Intent para carregamento da Activity
                    var intent = new Intent(this, typeof(MainActivity));

                    // Construindo PendingIntent
                    const int pendingIntentID = 0;
                    var pendingIntent = PendingIntent.GetActivity(this, pendingIntentID, intent, PendingIntentFlags.OneShot);

                    // Construindo a notificação
                    NotificationCompat.Builder builder = new NotificationCompat.Builder(this)
                        .SetContentIntent(pendingIntent)
                        .SetAutoCancel(true)
                        .SetContentTitle("Nova Promoção")
                        .SetSmallIcon(Resource.Drawable.ic_stat_bu_bling)
                        .SetContentText("Clique aqui para ter acesso a nova promoção");

                    if (bmpImagem != null && bmpImagem.ByteCount > 0)
                        builder.SetLargeIcon(bmpImagem);

                    if ((int)Android.OS.Build.VERSION.SdkInt >= 21)
                    {
                        builder.SetVisibility(0);
                        builder.SetCategory(Android.App.Notification.CategoryPromo);
                    }

                    // Publicando a notificação
                    NotificationManager notificationManager = 
                        (NotificationManager)GetSystemService("notification");
                    notificationManager.Notify(10, builder.Build());

                }, null, 0, 1800000);
        }

        public override Android.OS.IBinder OnBind(Android.Content.Intent intent)
        {
            throw new NotImplementedException();
        }
    }
}

