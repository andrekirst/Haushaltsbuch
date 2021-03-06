﻿using Haushaltsbuch.Library.Infrastructure.Interfaces;

namespace Haushaltsbuch.Domain.Haushaltsbuch.Commands
{
    public class ErstelleHaushaltsbuchCommand : Command
    {
        public string Name { get; set; }
        public string Währung { get; set; }

        public ErstelleHaushaltsbuchCommand(string name, string währung)
        {
            Name = name;
            Währung = währung;
        }

        private ErstelleHaushaltsbuchCommand()
        {
        }
    }
}