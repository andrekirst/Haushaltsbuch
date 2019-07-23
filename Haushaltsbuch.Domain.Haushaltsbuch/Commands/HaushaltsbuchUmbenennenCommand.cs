using Haushaltsbuch.Library.Infrastructure.Interfaces;

namespace Haushaltsbuch.Domain.Haushaltsbuch.Commands
{
    public class HaushaltsbuchUmbenennenCommand : Command
    {
        public string NeuerName { get; set; }

        private HaushaltsbuchUmbenennenCommand()
        {
        }

        public HaushaltsbuchUmbenennenCommand(string neuerName)
        {
            NeuerName = neuerName;
        }
    }
}