using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace bubling.Droid
{
    public static class CallAPI_Droid
    {
        private const string DOMAIN = "http://cpro21201.publiccloud.com.br/";

        private const string ENDERECO_LOGIN = "bu-bling/ws/bubling.asmx/Login?EMAIL={0}&SENHA={1}&REDE_SOCIAL={2}";
        private const string ENDERECO_HOME = "http://cpro21201.publiccloud.com.br/bu-bling/mobile/";
        private const string ENDERECO_PROMO = "bu-bling/ws/bubling.asmx/Retorna_Campanha_E_Imagem?EMAIL={0}";

        private const string ENDERECO_TESTE = "http://www.webservicex.net/globalweather.asmx/GetCitiesByCountry?CountryName=Brazil";

        /// <summary>
        ///  Executa a criação do objeto de chamada ao WebService
        /// </summary>
        /// <returns>Http client.</returns>
        /// <param name="tipoChamada">Tipo chamada(Enum).</param>
        internal static HttpClient CreateHttpClient(EnumCallAPI tipoChamada)
        {
            var conn = new HttpClient();
            var repo = new BuBlingRepository_Droid();

            switch (tipoChamada)
            {
                case EnumCallAPI.Home:
                    conn.BaseAddress = new Uri(ENDERECO_HOME);
                    break;

                case EnumCallAPI.Login:
                    conn.BaseAddress = new Uri(String.Concat(DOMAIN, ENDERECO_LOGIN));
                    break;

                case EnumCallAPI.Promo: 
                    conn.BaseAddress = new Uri(String.Concat(DOMAIN, String.Format(ENDERECO_PROMO,
                                String.IsNullOrEmpty(App.EMAIL_USUARIO) ? repo.RetornarEmailUsuarioLogado() : App.EMAIL_USUARIO)
                        ));
                    break;

                case EnumCallAPI.Teste: 
                    conn.BaseAddress = new Uri(ENDERECO_TESTE);
                    break;

                default:
                    break;
            }

            conn.Timeout = TimeSpan.FromSeconds(15);
            conn.DefaultRequestHeaders.Accept.Clear();
            conn.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));

            return conn;
        }

        /// <summary>
        /// Executa a chamada ao serviço
        /// </summary>
        /// <returns>ApiResult</returns>
        /// <param name="tipoChamada">Tipo chamada.</param>
        public static async Task<ApiResult> ExecChamadaAPI(EnumCallAPI tipoChamada)
        {
            var conn = CreateHttpClient(tipoChamada);
            ApiResult result = new ApiResult();

            using (conn)
            {   
                try
                {
                    var response = await conn.GetAsync(conn.BaseAddress);
                    
                    result = await response.Content.ReadAsAsync<ApiResult>();
                }
                catch (Exception ex)
                {
                    result = new ApiResult{ Erro = new ApiErroResult{ Mensagem = ex.Message } };
                }

                return result;
            }
        }

        /// <summary>
        /// Executa a chamada ao serviço (TESTE)
        /// </summary>
        /// <returns>String</returns>
        /// <param name="tipoChamada">Tipo chamada.</param>
        public static async Task<string> ExecChamadaTesteAPI(EnumCallAPI tipoChamada = EnumCallAPI.Teste)
        {
            var conn = CreateHttpClient(tipoChamada);

            using (conn)
            {   
                try
                {
                    var response = await conn.GetAsync(conn.BaseAddress);

                    if (String.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
                        return "Retorno vazio do servidor !";

                    return await response.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        /// <summary>
        /// Executa a chamada ao serviço e retorna uma STRING
        /// </summary>
        /// <returns>String</returns>
        /// <param name="tipoChamada">Tipo chamada.</param>
        public static async Task<string> ExecChamadaAPIComRetornoString(EnumCallAPI tipoChamada)
        {
            var conn = CreateHttpClient(tipoChamada);

            using (conn)
            {   
                try
                {
                    var response = await conn.GetAsync(conn.BaseAddress);

                    if (String.IsNullOrEmpty(await response.Content.ReadAsStringAsync()))
                        return "Retorno vazio do servidor !";

                    return await response.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }
    }
}

