using System;
using Haushaltsbuch.Library.Domain.Commandline;
using Haushaltsbuch.Library.Domain.Commands;
using Haushaltsbuch.Library.Infrastructure.Interfaces;
using Shouldly;
using Xunit;

namespace Haushaltsbuch.Tests.DomainTests.CommandlineTests
{
    public class CommandlineParserEinzahlungTest2
    {
        private readonly string[] _args = { "einzahlung", "01.01.2015", "400" };
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

        [Fact(DisplayName = "Das Einzahlungsdatum ist der 01.01.2015")]
        public void DasEinzahlungsdatumIstDer01_01_2015()
        {
            InHaushaltsbuchEinzahlenCommand command = _parser.Parse(args: _args) as InHaushaltsbuchEinzahlenCommand;
            command?.EinzahlungsDatum.ShouldBe(expected: new DateTime(year: 2015, month: 1, day: 1));
        }
    }
}