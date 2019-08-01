using Haushaltsbuch.Library.Infrastructure.Extensions;
using Haushaltsbuch.WebApi.Benutzerkonto.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Text.Encoding;

namespace Haushaltsbuch.UI.Web.Services.Benutzerkonto
{
    public class BenutzerkontoService : IBenutzerkontoService
    {
        private IHttpClientFactory HttpClientFactory { get; }

        public BenutzerkontoService(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
        }

        public async Task<bool> Registrieren(string anmeldenummer, string email, string passwortHash, string securityStamp)
        {
            HttpResponseMessage response = await Client.PostAsync(
                requestUri: "benutzerkonto",
                content: new StringContent(content: new RegistriereBenutzerModel
                    {
                        Anmeldenummer = anmeldenummer,
                        EMail = email,
                        PasswortHash = passwortHash,
                        SecurityStamp = securityStamp
                    }.ToJson(),
                    encoding: UTF8,
                    mediaType: Application.Json));
            return response.IsSuccessStatusCode;
        }

        public async Task<WebApi.Benutzerkonto.Models.Benutzerkonto> SucheAnhandAnmeldenummer(string anmeldenummer, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await Client.GetAsync(requestUri: $"benutzerkonto/findbyanmeldenummer/{anmeldenummer}", cancellationToken: cancellationToken);
            return response.IsSuccessStatusCode
                ? JsonConvert.DeserializeObject<WebApi.Benutzerkonto.Models.Benutzerkonto>(value: await response.Content.ReadAsStringAsync())
                : null;
        }

        public async Task<string> GenerateAnmeldenummer(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await Client.GetAsync(requestUri: "anmeldenummer/generate", cancellationToken: cancellationToken);
            return response.IsSuccessStatusCode
                ? (await response.Content.ReadAsStringAsync()).Trim(trimChar: '"')
                : null;
        }

        public async Task<bool> SetzeNormalisierteEMailAdresse(string anmeldenummer, string normalisierteEMail,
            CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await Client.PutAsync(
                requestUri: $"benutzerkonto/{anmeldenummer}/normalisierteemail",
                content: CreateStringContent(value: new SetzeNormalisierteEMailAdresseModel(normalisierteEMailAdresse: normalisierteEMail)),
                cancellationToken: cancellationToken);
            return response.IsSuccessStatusCode;
        }

        public Task<string> LiefereSecurityStamp(string anmeldenummer, CancellationToken cancellationToken) =>
            Client.GetStringAsync(requestUri: $"benutzerkonto/{anmeldenummer}/securitystamp");

        public Task<string> LiefereEMail(string anmeldenummer, CancellationToken cancellationToken) =>
            Client.GetStringAsync(requestUri: $"benutzerkonto/{anmeldenummer}/email");

        public async Task<WebApi.Benutzerkonto.Models.Benutzerkonto> SucheAnhandEMail(string email, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await Client.GetAsync(requestUri: $"benutzerkonto/suche/email/{email}",
                cancellationToken: cancellationToken);

            return response.IsSuccessStatusCode
                ? JsonConvert.DeserializeObject<WebApi.Benutzerkonto.Models.Benutzerkonto>(value: await response.Content.ReadAsStringAsync())
                : null;
        }

        public async Task<string> LiefereUserName(string anmeldenummer, CancellationToken cancellationToken)
        {
            string username = await Client.GetStringAsync(requestUri: $"benutzerkonto/{anmeldenummer}/username");
            return username.Trim(trimChar: '"');
        }

        public async Task<string> LiefereUserId(string anmeldenummer, CancellationToken cancellationToken)
        {
            string userid = await Client.GetStringAsync(requestUri: $"benutzerkonto/{anmeldenummer}/userid");
            return userid.Trim(trimChar: '"');
        }

        public async Task<string> LieferePasswordHash(string anmeldenummer, CancellationToken cancellationToken)
        {
            string passwordHash = await Client.GetStringAsync(requestUri: $"benutzerkonto/{anmeldenummer}/passwordhash");
            return passwordHash
                .Trim(trimChar: '"')
                .Replace(oldValue: @"\u002f", newValue: "/")
                .Replace(oldValue: @"\u002b", newValue: "+");
        }

        private HttpClient Client => HttpClientFactory.CreateClient(name: "HaushaltsbuchAPI.Benutzerkonto");

        private static StringContent CreateStringContent(object value) =>
            new StringContent(content: value.ToJson(),
                encoding: UTF8,
                mediaType: Application.Json);
    }
}