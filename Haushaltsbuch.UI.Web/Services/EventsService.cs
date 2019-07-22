using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static System.Text.Encoding;
using Convert = System.Convert;

namespace Haushaltsbuch.UI.Web.Services
{
    public class EventsService : IEventsService
    {
        private IHttpClientFactory HttpClientFactory { get; }

        public EventsService(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
        }

        public async Task<List<string>> GetEventsRawData()
        {
            HttpResponseMessage response = await Client.GetAsync(requestUri: "events");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                string[] items = content.Split(separator: "\",\"");
                items[0] = new string(value: items[0].Skip(count: 2).ToArray());
                items[items.Length - 1] = new string(value: items[items.Length - 1].Take(count: items[items.Length - 1].Length - 2).ToArray());

                return items
                    .Select(selector: s => s
                        .Replace(oldValue: @"\u002b", newValue: "+")
                        .Replace(oldValue: @"\u002f", newValue: "/"))
                    .Select(selector: Convert.FromBase64String)
                    .Select(selector: b => UTF8.GetString(bytes: b))
                    .ToList();
            }

            return new List<string>();
        }

        private HttpClient Client => HttpClientFactory.CreateClient(name: "HaushaltsbuchAPI");
    }
}