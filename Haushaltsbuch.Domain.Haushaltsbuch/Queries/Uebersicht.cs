using System.Collections.Generic;

namespace Haushaltsbuch.Domain.Haushaltsbuch.Queries
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