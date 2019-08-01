using System;
using Haushaltsbuch.Library.Infrastructure.Interfaces;

namespace Haushaltsbuch.Library.Infrastructure.Implementations
{
    public class DefaultDateTimeOffsetProvider : IDateTimeOffsetProvider
    {
        public DateTimeOffset Now => DateTimeOffset.Now;
    }
}