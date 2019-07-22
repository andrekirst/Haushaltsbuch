﻿using System;
using Haushaltsbuch.Library.Infrastructure.Extensions;
using Haushaltsbuch.Library.Infrastructure.Interfaces;
using static System.Guid;

namespace Haushaltsbuch.Library.Domain
{
    public class HaushaltsbuchId : IAggregateId
    {
        private const string Prefix = "Haushaltsbuch-";

        private HaushaltsbuchId(Guid id) => Id = id;

        public HaushaltsbuchId(string id)
        {
            Id = Parse(input: id.RemovePrefix(prefix: Prefix));
        }

        public Guid Id { get; private set; }

        public string Identifier => $"{Prefix}{Id}";

        public override string ToString() => Identifier;

        public override bool Equals(object obj) =>
            obj is HaushaltsbuchId other
            && Equals(objA: Id, objB: other.Id);

        public override int GetHashCode() => Id.GetHashCode();

        public static HaushaltsbuchId NeueHaushaltsbuchId() => new HaushaltsbuchId(id: NewGuid());
    }
}