using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using CalendarioInstitucional.Data;
using CalendarioInstitucional.Model;

namespace CalendarioInstitucional.Repository
{
    public class SistemaREP
    {
        #region Services
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly AcessaDados _acessaDados;
        #endregion

        #region Constructor
        public SistemaREP(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _acessaDados = new AcessaDados(_configuration);
            _httpClient = _acessaDados.conexaoWebApiLogin();
        }
        #endregion

        #region Methods

        #region BuscarPorCodigo
        public async Task<SistemaMOD> BuscarPorCodigo(int CdSistema)
        {
            SistemaMOD Sistema = new SistemaMOD();

            using (var response = await _httpClient.GetAsync($"Sites/api/Sistema/BuscarSistemaPorCodigo?cdSistema={CdSistema}"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                Sistema = JsonConvert.DeserializeObject<SistemaMOD>(apiResponse);
            }

            return Sistema;
        }
        #endregion

        #endregion
    }
}
