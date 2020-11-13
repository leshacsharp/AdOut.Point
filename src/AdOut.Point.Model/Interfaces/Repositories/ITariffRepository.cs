﻿using AdOut.Point.Model.Database;
using AdOut.Point.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdOut.Point.Model.Interfaces.Repositories
{
    public interface ITariffRepository : IBaseRepository<Tariff>
    {
        Task<Tariff> GetByIdAsync(string tariffId);

        Task<List<PlanTariffDto>> GetAdPointTariffsAsync(string planId);
    }
}
