using System;
using Haushaltsbuch.Library.Domain.Commandline;
using Haushaltsbuch.Library.Domain.Commands;
using Haushaltsbuch.Library.Infrastructure.Interfaces;
using Shouldly;
using Xunit;

namespace Haushaltsbuch.Tests.DomainTests.CommandlineTests
{
    public class CommandlineParserAuszahlungTest2
    {
        private readonly string[] _args = { "auszahlung", "01.01.2015", "700", "Miete" };
        private readonly CommandlineParser _parser = new CommandlineParser();

        [Fact(DisplayName = "Ist vom Typ AusHaltshaltsbuchAuszahlenCommand")]
        public void IstVomTypAusHaltshaltsbuchAuszahlenCommand()
        {
            Command command = _parser.Parse(args: _args);

            command.ShouldBeOfType(expected: typeof(AusHaltshaltsbuchAuszahlenCommand));
        }

        [Fact(DisplayName = "Der Betrag ist 700")]
        public void HatDenBetrag700()
        {
            AusHaltshaltsbuchAuszahlenCommand command = _parser.Parse(args: _args) as AusHaltshaltsbuchAuszahlenCommand;
            command?.Betrag.ShouldBe(expected: 700);
        }

        [Fact(DisplayName = "Die Kategorie ist \"Miete\"")]
        public void HatDieKategorieMiete()
        {
            AusHaltshaltsbuchAuszahlenCommand command = _parser.Parse(args: _args) as AusHaltshaltsbuchAuszahlenCommand;
            command?.Kategorie.ShouldBe(expected: "Miete");
        }

        [Fact(DisplayName = "Der Memotext ist nicht gesetzt")]
        public void HatKeinenMemotext()
        {
            AusHaltshaltsbuchAuszahlenCommand command = _parser.Parse(args: _args) as AusHaltshaltsbuchAuszahlenCommand;
            command?.Memotext.ShouldBeNull();
        }

        [Fact(DisplayName = "Das Auszahlungsdatum ist der 01.01.2015")]
        public void DasAuszahlungsdatumIstDer01_01_2015()
        {
            AusHaltshaltsbuchAuszahlenCommand command = _parser.Parse(args: _args) as AusHaltshaltsbuchAuszahlenCommand;
            command?.Auszahlungsdatum.ShouldBe(expected: new DateTime(year: 2015, month: 1, day: 1));
        }
    }
}