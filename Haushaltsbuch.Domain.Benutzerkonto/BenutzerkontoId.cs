using System;
using Haushaltsbuch.Library.Infrastructure.Extensions;
using Haushaltsbuch.Library.Infrastructure.Interfaces;

namespace Haushaltsbuch.Domain.Benutzerkonto
{
    public class BenutzerkontoId : IAggregateId
    {
        private const string Prefix = "Benutzerkonto-";

        private BenutzerkontoId(Guid id) => Id = id;

        public BenutzerkontoId(string id)
        {
            Id = Guid.Parse(input: id.RemovePrefix(prefix: Prefix));
        }

        public Guid Id { get; private set; }

        public string Identifier => $"{Prefix}{Id}";

        public override string ToString() => Identifier;

        public override bool Equals(object obj) =>
            obj is BenutzerkontoId other
            && Equals(objA: Id, objB: other.Id);

        public override int GetHashCode() => Id.GetHashCode();

        public static BenutzerkontoId NeueBenutzerkontoId() => new BenutzerkontoId(id: Guid.NewGuid());
    }
}