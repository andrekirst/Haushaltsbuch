using Haushaltsbuch.Library.Infrastructure.Interfaces;

namespace Haushaltsbuch.Library.Domain.Commandline
{
    public interface ICommandlineParser
    {
        Command Parse(string[] args);
    }
}