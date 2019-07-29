using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Haushaltsbuch.Domain.Haushaltsbuch.ReadModel;
using Newtonsoft.Json;

namespace Haushaltsbuch.UI.Web.Services.Benutzerkonto
{
    public class BenutzerkontoService : IBenutzerkontoService
    {
        private IHttpClientFactory HttpClientFactory { get; }

        public BenutzerkontoService(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
        }

        public async Task<WebApi.Benutzerkonto.Models.Benutzerkonto> FindByNameAsync(string anmeldenummer, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await Client.GetAsync(requestUri: $"benutzerkonto/findbyname/{anmeldenummer}", cancellationToken: cancellationToken);
            return response.IsSuccessStatusCode
                ? JsonConvert.DeserializeObject<WebApi.Benutzerkonto.Models.Benutzerkonto>(value: await response.Content.ReadAsStringAsync())
                : null;
        }

        private HttpClient Client => HttpClientFactory.CreateClient(name: "HaushaltsbuchAPI.Benutzerkonto");
    }
}