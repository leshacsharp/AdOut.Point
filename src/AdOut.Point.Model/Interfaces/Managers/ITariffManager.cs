using AdOut.Point.Model.Api;
using AdOut.Point.Model.Classes;
using AdOut.Point.Model.Database;
using System;
using System.Threading.Tasks;

namespace AdOut.Point.Model.Interfaces.Managers
{
    public interface ITariffManager : IBaseManager<Tariff>
    {
        Task<ValidationResult<string>> ValidateTariff(int adPointId, TimeSpan startTime, TimeSpan endTime);

        Task CreateAsync(CreateTariffModel createModel);

        Task UpdateAsync(UpdateTariffModel updateModel);

        Task DeleteAsync(int tariffId);
    }
}
