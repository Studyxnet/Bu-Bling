using System;
using Android.Widget;
using Android.App;
using Android.Util;
using Xamarin.Forms;
using TaskStackBuilder = Android.Support.V4.App.TaskStackBuilder;
using Android.Support.V4.App;

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

        public void CallWebService()
        {
            _timer = new System.Threading.Timer((o) =>
                {
                    // Construindo a notificação
                    NotificationCompat.Builder builder = new NotificationCompat.Builder(this)
                        .SetAutoCancel(true)
                        .SetContentTitle("Aviso")
                        .SetSmallIcon(Resource.Drawable.notification)
                        .SetContentText("Mensagem por Notificação");

                    // Publicando a notificação
                    NotificationManager notificationManager = 
                        (NotificationManager)GetSystemService("notification");
                    notificationManager.Notify(10, builder.Build());

                }, null, 0, 4000);
        }

        public override Android.OS.IBinder OnBind(Android.Content.Intent intent)
        {
            throw new NotImplementedException();
        }
    }
}

