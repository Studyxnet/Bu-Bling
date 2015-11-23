using System;

namespace bubling.Droid
{
    public class ApiResult
    {
        public string URL { get; set; }

        public byte[] Image { get; set; }

        public ApiErroResult Erro { get; set; }

        public ApiResult()
        {
        }
    }
}

