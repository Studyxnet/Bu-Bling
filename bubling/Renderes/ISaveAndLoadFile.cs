using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace bubling
{
    public interface ISaveAndLoadFile
    {
        string GetImage(string imageName);

        byte[] GetImageArray(string imageName);

        bool BaixaImagemSalvarEmDisco(string imagem, string url);
    }
}

