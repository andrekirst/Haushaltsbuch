using Haushaltsbuch.Library.Domain.Commandline;
using Haushaltsbuch.Library.Domain.Commands;
using Haushaltsbuch.Library.Infrastructure.Interfaces;
using Shouldly;
using Xunit;

namespace Haushaltsbuch.Tests.DomainTests.CommandlineTests
{
    public class CommandlineParserEinzahlungTest1
    {
        private readonly string[] _args = { "einzahlung", "400" };
        private readonly CommandlineParser _parser = new CommandlineParser();

        [Fact(DisplayName = "Ist vom Typ InHaushaltsbuchEinzahlenCommand")]
        public void IstVomTypInHaushaltsbuchEinzahlenCommand()
        {
            Command command = _parser.Parse(args: _args);
            command.ShouldBeOfType(expected: typeof(InHaushaltsbuchEinzahlenCommand));
        }

        [Fact(DisplayName = "Der Betrag ist 400")]
        public void HatDenBetrag400()
        {
            InHaushaltsbuchEinzahlenCommand command = _parser.Parse(args: _args) as InHaushaltsbuchEinzahlenCommand;
            command?.Betrag.ShouldBe(expected: 400);
        }
    }
}