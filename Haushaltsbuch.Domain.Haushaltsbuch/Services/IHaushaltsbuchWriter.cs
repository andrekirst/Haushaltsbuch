using System;
using System.Threading.Tasks;

namespace Haushaltsbuch.Library.Domain.Services
{
    public interface IHaushaltsbuchWriter
    {
        Task ErstellenAsync(string name, Währung währung);
        Task EinzahlenAsync(string haushaltsbuchId, double betrag, DateTimeOffset? einzahlungsdatum);
        Task AuszahlenAsync(string haushaltsbuchId, double betrag, DateTimeOffset? auszahlungsdatum, Kategorie kategorie, string memotext);
        Task Umbenennen(string haushaltsbuchId, string neuerName);
    }
}