namespace Haushaltsbuch.WebApi.Models.Dto.Commands
{
    public class HaushaltsbuchUmbenennenCommand
    {
        public string NeuerName { get; set; }

        public HaushaltsbuchUmbenennenCommand()
        {
        }

        public HaushaltsbuchUmbenennenCommand(string neuerName)
        {
            NeuerName = neuerName;
        }
    }
}