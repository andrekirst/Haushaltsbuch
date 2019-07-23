namespace Haushaltsbuch.Domain.Haushaltsbuch
{
    public class Währung
    {
        public string Symbol { get; set; }
        public string Name { get; set; }

        private Währung()
        {
        }

        public Währung(string symbol, string name)
        {
            Symbol = symbol;
            Name = name;
        }

        public override int GetHashCode() => Name.GetHashCode();

        public override bool Equals(object obj) =>
            obj is Währung other
            && Equals(objA: Name, objB: other.Name);

        public override string ToString() => $"{Symbol} ({Name})";
    }
}