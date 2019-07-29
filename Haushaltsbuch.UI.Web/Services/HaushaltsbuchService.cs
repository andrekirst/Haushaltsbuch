using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Haushaltsbuch.Library.Infrastructure.Extensions;
using Haushaltsbuch.WebApi.Haushaltsbuch.Models.Dto;
using Haushaltsbuch.WebApi.Haushaltsbuch.Models.Dto.Commands;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using HaushaltsbuchAuszahlung = Haushaltsbuch.Domain.Haushaltsbuch.ReadModel.HaushaltsbuchAuszahlung;
using HaushaltsbuchEinzahlung = Haushaltsbuch.Domain.Haushaltsbuch.ReadModel.HaushaltsbuchEinzahlung;

namespace Haushaltsbuch.UI.Web.Services
{
    public class HaushaltsbuchService : IHaushaltsbuchService
    {
        private IHttpClientFactory HttpClientFactory { get; }

        public HaushaltsbuchService(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
        }

        public async Task<bool> ErstelleHaushaltsbuch(string name, string währung)
        {
            HttpResponseMessage response = await Client.PostAsync(requestUri: "haushaltsbuch", content: new StringContent(
                content: new { name, währung }.ToJson(), encoding: Encoding.UTF8, mediaType: Application.Json));

            return response.IsSuccessStatusCode;
        }

        public async Task<List<Domain.Haushaltsbuch.ReadModel.Haushaltsbuch>> GetAllHaushaltsbuecher()
        {
            HttpResponseMessage response = await Client.GetAsync(requestUri: "haushaltsbuch");

            return response.IsSuccessStatusCode
                ? JsonConvert.DeserializeObject<List<Domain.Haushaltsbuch.ReadModel.Haushaltsbuch>>(value: await response.Content.ReadAsStringAsync())
                : null;
        }

        public async Task<Domain.Haushaltsbuch.ReadModel.Haushaltsbuch> GetHaushaltsbuchById(string haushaltsbuchId)
        {
            HttpResponseMessage response = await Client.GetAsync(requestUri: $"haushaltsbuch/{haushaltsbuchId}");

            return response.IsSuccessStatusCode
                ? JsonConvert.DeserializeObject<Domain.Haushaltsbuch.ReadModel.Haushaltsbuch>(value: await response.Content.ReadAsStringAsync())
                : null;
        }

        public async Task<Domain.Haushaltsbuch.ReadModel.Haushaltsbuch> GetHaushaltsbuchByName(string name)
        {
            HttpResponseMessage response = await Client.GetAsync(requestUri: $"haushaltsbuch/search?name={name}");

            return response.IsSuccessStatusCode
                ? JsonConvert.DeserializeObject<Domain.Haushaltsbuch.ReadModel.Haushaltsbuch>(
                    value: await response.Content.ReadAsStringAsync())
                : null;
        }

        public async Task<List<HaushaltsbuchAuszahlung>> GetAuszahlungen(string haushaltsbuchId)
        {
            HttpResponseMessage response = await Client.GetAsync(requestUri: $"auszahlungen/{haushaltsbuchId}");

            return response.IsSuccessStatusCode
                ? JsonConvert.DeserializeObject<List<HaushaltsbuchAuszahlung>>(value: await response.Content.ReadAsStringAsync())
                : null;
        }

        public async Task<List<HaushaltsbuchEinzahlung>> GetEinzahlungen(string haushaltsbuchId)
        {
            HttpResponseMessage response = await Client.GetAsync(requestUri: $"einzahlungen/{haushaltsbuchId}");

            return response.IsSuccessStatusCode
                ? JsonConvert.DeserializeObject<List<HaushaltsbuchEinzahlung>>(value: await response.Content.ReadAsStringAsync())
                : null;
        }

        public Task<List<Währung>> GetWährungen() =>
            Client.GetJsonAsync<List<Währung>>(requestUri: "waehrungen");

        public async Task<bool> Einzahlen(string haushaltsbuchId, InHaushaltsbuchEinzahlenCommand command)
        {
            HttpResponseMessage response = await Client.PostAsync(
                requestUri: $"einzahlungen/{haushaltsbuchId}",
                content: new StringContent(content: command.ToJson(), encoding: Encoding.UTF8, mediaType: Application.Json));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Auszahlen(string haushaltsbuchId, AusHaltshaltsbuchAuszahlenCommand command)
        {
            HttpResponseMessage response = await Client.PostAsync(
                requestUri: $"auszahlungen/{haushaltsbuchId}",
                content: new StringContent(
                    content: command.ToJson(),
                    encoding: Encoding.UTF8,
                    mediaType: Application.Json));

            return response.IsSuccessStatusCode;
        }

        public Task<double> Kassenbestand(string haushaltsbuchId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> HaushaltsbuchUmbenennen(string haushaltsbuchId, HaushaltsbuchUmbenennenCommand command)
        {
            HttpResponseMessage response = await Client.PutAsync(requestUri: $"haushaltsbuch/{haushaltsbuchId}/name",
                content: new StringContent(
                    content: command.ToJson(),
                    encoding: Encoding.UTF8,
                    mediaType: Application.Json));
            return response.IsSuccessStatusCode;
        }

        private HttpClient Client => HttpClientFactory.CreateClient(name: "HaushaltsbuchAPI.Haushaltsbuch");
    }
}