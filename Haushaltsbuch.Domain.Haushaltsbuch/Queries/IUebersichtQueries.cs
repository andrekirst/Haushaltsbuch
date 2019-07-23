namespace Haushaltsbuch.Domain.Haushaltsbuch.Queries
{
    public interface IUebersichtQueries
    {
        Uebersicht AktuellerMonat(string haushaltsbuchId);

        Uebersicht FuerMonat(string haushaltsbuchId, int monat, int jahr);

        AktuellerKassenbestand AktuellerKassenbestand(string haushaltsbuchId);

        KategorieAusgabe KategorieAusgabe(string haushaltsbuchId, string kategorie);
    }
}