namespace Haushaltsbuch.Domain.Haushaltsbuch
{
    public class Kategorie
    {
        public string Name { get; }

        public Kategorie(string name)
        {
            Name = name;
        }

        public override int GetHashCode() => Name.GetHashCode();

        public override bool Equals(object obj) =>
            obj is Kategorie other
            && Equals(objA: Name, objB: other.Name);
    }
}