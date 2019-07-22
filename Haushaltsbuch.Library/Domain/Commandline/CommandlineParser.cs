using System;
using System.Globalization;
using System.Linq;
using Haushaltsbuch.Library.Domain.Commands;
using Haushaltsbuch.Library.Infrastructure.Interfaces;

namespace Haushaltsbuch.Library.Domain.Commandline
{
    public class CommandlineParser : ICommandlineParser
    {
        public Command Parse(string[] args)
        {
            if (args.Contains(value: "auszahlung"))
            {
                return ParseAuszahlung(args: args);
            }

            if (args.Contains(value: "einzahlung"))
            {
                return ParseEinzahlung(args: args);
            }
            
            throw new NotImplementedException();
        }

        private static InHaushaltsbuchEinzahlenCommand ParseEinzahlung(string[] args)
        {
            args = args.Skip(count: 1).ToArray();

            DateTime? auszahlungsdatum = null;
            if (DateTime.TryParse(s: args.First(), result: out DateTime tmpDate))
            {
                auszahlungsdatum = tmpDate;
                args = args.Skip(count: 1).ToArray();
            }

            if (double.TryParse(
                s: args.First(),
                style: NumberStyles.Any,
                provider: new CultureInfo(name: "de-DE"),
                result: out double betrag))
            {
                args = args.Skip(count: 1).ToArray();
            }

            return new InHaushaltsbuchEinzahlenCommand(betrag: betrag, einzahlungsDatum: auszahlungsdatum);
        }

        private static AusHaltshaltsbuchAuszahlenCommand ParseAuszahlung(string[] args)
        {
            args = args.Skip(count: 1).ToArray();

            DateTime? auszahlungsdatum = null;
            if (DateTime.TryParse(s: args.First(), result: out DateTime tmpDate))
            {
                auszahlungsdatum = tmpDate;
                args = args.Skip(count: 1).ToArray();
            }

            if (double.TryParse(
                s: args.First(),
                style: NumberStyles.Any,
                provider: new CultureInfo(name: "de-DE"),
                result: out double betrag))
            {
                args = args.Skip(count: 1).ToArray();
            }

            string kategorie = args.Any() ? args.First() : null;
            args = args.Skip(count: 1).ToArray();

            string memotext = args.Any() ? args.First() : null;

            return new AusHaltshaltsbuchAuszahlenCommand(
                betrag: betrag,
                kategorie: kategorie,
                memotext: memotext,
                auszahlungsdatum: auszahlungsdatum);
        }
    }
}