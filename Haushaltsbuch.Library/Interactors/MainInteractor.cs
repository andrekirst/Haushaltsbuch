using System;
using System.Threading.Tasks;
using Haushaltsbuch.Library.Domain;
using Haushaltsbuch.Library.Domain.Commandline;
using Haushaltsbuch.Library.Domain.Commands;
using Haushaltsbuch.Library.Domain.Queries;
using Haushaltsbuch.Library.Domain.Services;
using Haushaltsbuch.Library.Infrastructure.Interfaces;

namespace Haushaltsbuch.Library.Interactors
{
    public class MainInteractor : IMainInteractor
    {
        private ICommandlineParser CommandlineParser { get; }
        private IHaushaltsbuchReader HaushaltsbuchReader { get; }
        private IHaushaltsbuchWriter HaushaltsbuchWriter { get; }
        private IUebersichtQueries UebersichtQueries { get; }

        public MainInteractor(
            ICommandlineParser commandlineParser,
            IHaushaltsbuchReader haushaltsbuchReader,
            IHaushaltsbuchWriter haushaltsbuchWriter,
            IUebersichtQueries uebersichtQueries)
        {
            CommandlineParser = commandlineParser;
            HaushaltsbuchReader = haushaltsbuchReader;
            HaushaltsbuchWriter = haushaltsbuchWriter;
            UebersichtQueries = uebersichtQueries;
        }

        public async Task ExecuteAsync(string[] args)
        {
            Command command = CommandlineParser.Parse(args: args);

            Domain.ReadModel.Haushaltsbuch haushaltsbuch = await GetHaushaltsbuch();

            bool commandSuccessfullyExecuted = await ExecuteCommand(command: command, haushaltsbuch: haushaltsbuch);

            if (commandSuccessfullyExecuted)
            {
                double kassenbestand = UebersichtQueries.AktuellerKassenbestand(haushaltsbuchId: haushaltsbuch.Id).Kassenbestand;
                Console.WriteLine(value: $"Kassenbestand: {kassenbestand}");

                if (command is AusHaltshaltsbuchAuszahlenCommand ausHaltshaltsbuchAuszahlenCommand)
                {
                    KategorieAusgabe kategorieAusgabe = UebersichtQueries.KategorieAusgabe(haushaltsbuchId: haushaltsbuch.Id,
                        kategorie: ausHaltshaltsbuchAuszahlenCommand.Kategorie);

                    Console.WriteLine(value: $"{kategorieAusgabe.Kategorie}: {kategorieAusgabe.Gesamtausgaben}");
                }
            }
        }

        private async Task<Domain.ReadModel.Haushaltsbuch> GetHaushaltsbuch()
        {
            Domain.ReadModel.Haushaltsbuch haushaltsbuch = await HaushaltsbuchReader.GetDefault();
            if (haushaltsbuch != null)
            {
                return haushaltsbuch;
            }

            await HaushaltsbuchWriter.ErstellenAsync(name: "default", währung: new Währung(symbol: "€", name: "EUR"));
            return await HaushaltsbuchReader.GetDefault();
        }

        private async Task<bool> ExecuteCommand(Command command, Domain.ReadModel.Haushaltsbuch haushaltsbuch)
        {
            switch (command)
            {
                case AusHaltshaltsbuchAuszahlenCommand auszahlungCommand:
                    await HaushaltsbuchWriter.AuszahlenAsync(
                        haushaltsbuchId: haushaltsbuch.Id,
                        betrag: auszahlungCommand.Betrag,
                        auszahlungsdatum: auszahlungCommand.Auszahlungsdatum,
                        kategorie: new Kategorie(name: auszahlungCommand.Kategorie),
                        memotext: auszahlungCommand.Memotext);
                    return true;
                case InHaushaltsbuchEinzahlenCommand einzahlenCommand:
                    await HaushaltsbuchWriter.EinzahlenAsync(
                        haushaltsbuchId: haushaltsbuch.Id,
                        betrag: einzahlenCommand.Betrag,
                        einzahlungsdatum: einzahlenCommand.EinzahlungsDatum);
                    return true;
            }

            return false;
        }
    }
}