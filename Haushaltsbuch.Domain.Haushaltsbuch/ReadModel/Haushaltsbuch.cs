namespace Haushaltsbuch.Domain.Haushaltsbuch.ReadModel
{
    public class Haushaltsbuch : IReadEntity
    {
        public string Id { get; set; }

        public double Kassenbestand { get; set; }

        public string Name { get; set; }

        public string WährungName { get; set; }

        public string WährungSymbol { get; set; }

        public override string ToString() => $"Name: {Name}, Kassenbestand: {Kassenbestand} {WährungSymbol} (Id: {Id})";
    }
}