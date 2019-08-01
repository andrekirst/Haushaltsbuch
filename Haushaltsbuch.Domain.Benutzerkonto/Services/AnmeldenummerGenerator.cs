using System;
using System.Linq;
using Haushaltsbuch.Library.Infrastructure.Interfaces;

namespace Haushaltsbuch.Domain.Benutzerkonto.Services
{
    public class AnmeldenummerGenerator : IAnmeldenummerGenerator
    {
        private IDateTimeOffsetProvider DateTimeOffsetProvider { get; }

        public AnmeldenummerGenerator(
            IDateTimeOffsetProvider dateTimeOffsetProvider)
        {
            DateTimeOffsetProvider = dateTimeOffsetProvider;
        }

        public string Generate()
        {
            DateTimeOffset now = DateTimeOffsetProvider.Now;
            return new string(value: now.ToString(format: "ddMMyyyyHHmmssffff").Reverse().ToArray());
        }
    }
}