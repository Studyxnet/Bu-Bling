using System;
using Xamarin.Forms;
using bubling.Droid;
using System.Threading.Tasks;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using System.Net;
using System.IO;

[assembly: Dependency(typeof(SaveAndLoadFile_Droid))]
namespace bubling.Droid
{
    public class SaveAndLoadFile_Droid : ISaveAndLoadFile
    {
        Bitmap imagem;

        #region ISaveAndLoadFile implementation

        public string GetImage(string imageName)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var nomeImagem = System.IO.Path.Combine(path, imageName);

            return nomeImagem;
        }

        public byte[] GetImageArray(string imageName)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var nomeImagem = System.IO.Path.Combine(path, imageName);

            return System.IO.File.ReadAllBytes(nomeImagem);
        }

        public bool BaixaImagemSalvarEmDisco(string imagem, string url)
        {
            using (var client = new WebClient())
            {
                var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var nomeImagem = System.IO.Path.Combine(path, imagem);

                var imageBytes = client.DownloadData(new Uri(url));
                Bitmap imagemBitmap = null;

                using (var ms = new MemoryStream(imageBytes))
                {
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        var options = new BitmapFactory.Options { InSampleSize = 2 };
                        imagemBitmap = BitmapFactory.DecodeStream(ms, null, options);
                        //imagemBitmap.Compress(Bitmap.CompressFormat.Png, 70, ms);
                    }

                    try
                    {
                        if (imagemBitmap != null)
                        {
                            System.IO.File.WriteAllBytes(nomeImagem, ms.ToArray());
                            return true;
                        }

                        return false;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        #endregion
    }
}
