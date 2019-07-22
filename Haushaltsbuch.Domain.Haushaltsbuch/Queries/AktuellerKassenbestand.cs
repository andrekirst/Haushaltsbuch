namespace Haushaltsbuch.Library.Domain.Queries
{
    public class AktuellerKassenbestand
    {
        public double Kassenbestand { get; }

        public AktuellerKassenbestand(double kassenbestand)
        {
            Kassenbestand = kassenbestand;
        }
    }
}