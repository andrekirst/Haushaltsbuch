using System.Collections.Generic;
using System.Threading.Tasks;

namespace Haushaltsbuch.UI.Web.Services
{
    public interface IEventsService
    {
        Task<List<string>> GetEventsRawData();
    }
}