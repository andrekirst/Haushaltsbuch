using System;

namespace Haushaltsbuch.Library.Infrastructure.Interfaces
{
    public interface IDateTimeOffsetProvider
    {
        DateTimeOffset Now { get; }
    }
}