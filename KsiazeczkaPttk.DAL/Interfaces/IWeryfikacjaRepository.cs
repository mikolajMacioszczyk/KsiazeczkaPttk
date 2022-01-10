﻿using KsiazeczkaPttk.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KsiazeczkaPttk.DAL.Interfaces
{
    public interface IWeryfikacjaRepository
    {
        Task<IEnumerable<WycieczkaPreview>> GetAllNieZweryfikowaneWycieczki();

        Task<Wycieczka> GetWeryfikowanaWycieczkaById(int wycieczkaId);

        Task<Result<Weryfikacja>> CreateWeryfikacja(Weryfikacja weryfikacja);
    }
}
