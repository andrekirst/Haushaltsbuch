using System.Collections.Generic;

namespace Haushaltsbuch.Library.Domain.Queries
{
    public class Uebersicht
    {
        public double Kassenbestand { get; }
        public Dictionary<string, double> Eintraege { get; }

        public Uebersicht(double kassenbestand, Dictionary<string, double> eintraege)
        {
            Kassenbestand = kassenbestand;
            Eintraege = eintraege;
        }
    }
}