﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Haushaltsbuch.Domain.Haushaltsbuch.Services
{
    public interface IHaushaltsbuchReader
    {
        Task<IEnumerable<ReadModel.Haushaltsbuch>> GetAllAsync();
        Task<ReadModel.Haushaltsbuch> GetByIdAsync(string id);
        Task<ReadModel.Haushaltsbuch> GetByName(string name);
        Task<ReadModel.Haushaltsbuch> GetDefault();
        Task<IEnumerable<ReadModel.HaushaltsbuchAuszahlung>> GetAuszahlungenOfAsync(string haushaltsbuchId);
        Task<IEnumerable<ReadModel.HaushaltsbuchEinzahlung>> GetEinzahlungenOfAsync(string haushaltsbuchId);
        Task<IEnumerable<Währung>> GetAllWährungen();
        Task<Währung> GetWährungByName(string name);
    }
}