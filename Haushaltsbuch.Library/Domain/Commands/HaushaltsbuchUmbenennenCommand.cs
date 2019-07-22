using Haushaltsbuch.Library.Infrastructure.Interfaces;

namespace Haushaltsbuch.Library.Domain.Commands
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