using Haushaltsbuch.Library.Domain.Commandline;
using Haushaltsbuch.Library.Domain.Commands;
using Haushaltsbuch.Library.Infrastructure.Interfaces;
using Shouldly;
using Xunit;

namespace Haushaltsbuch.Tests.DomainTests.CommandlineTests
{
    public class CommandlineParserAuszahlungTest1
    {
        private readonly string[] _args = { "auszahlung", "5,99", "Restaurantbesuche", "Schokobecher" };
        private readonly CommandlineParser _parser = new CommandlineParser();

        [Fact(DisplayName = "Ist vom Typ AusHaltshaltsbuchAuszahlenCommand")]
        public void IstVomTypAusHaltshaltsbuchAuszahlenCommand()
        {
            Command command = _parser.Parse(args: _args);

            command.ShouldBeOfType(expected: typeof(AusHaltshaltsbuchAuszahlenCommand));
        }

        [Fact(DisplayName = "Der Betrag ist 5,99")]
        public void HatDenBetrag5_99()
        {
            AusHaltshaltsbuchAuszahlenCommand command = _parser.Parse(args: _args) as AusHaltshaltsbuchAuszahlenCommand;
            command?.Betrag.ShouldBe(expected: 5.99);
        }

        [Fact(DisplayName = "Die Kategorie ist \"Restaurantbesuche\"")]
        public void HatDieKategorieRestaurantbesuche()
        {
            AusHaltshaltsbuchAuszahlenCommand command = _parser.Parse(args: _args) as AusHaltshaltsbuchAuszahlenCommand;
            command?.Kategorie.ShouldBe(expected: "Restaurantbesuche");
        }

        [Fact(DisplayName = "Der Memotext ist \"Schokobecher\"")]
        public void HatDenMemotextSchokobecher()
        {
            AusHaltshaltsbuchAuszahlenCommand command = _parser.Parse(args: _args) as AusHaltshaltsbuchAuszahlenCommand;
            command?.Memotext.ShouldBe(expected: "Schokobecher");
        }

        [Fact(DisplayName = "Das Auszahlungsdatum ist nicht gesetzt")]
        public void AuszahlungsdatumIstNichtGesetzt()
        {
            AusHaltshaltsbuchAuszahlenCommand command = _parser.Parse(args: _args) as AusHaltshaltsbuchAuszahlenCommand;
            command?.Auszahlungsdatum.ShouldBeNull();
        }
    }
}