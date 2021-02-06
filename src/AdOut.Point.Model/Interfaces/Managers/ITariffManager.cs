using AdOut.Point.Model.Api;
using AdOut.Point.Model.Classes;
using System;
using System.Threading.Tasks;

namespace AdOut.Point.Model.Interfaces.Managers
{
    public interface ITariffManager 
    {
        Task<ValidationResult<string>> ValidateTariff(string adPointId, TimeSpan startTime, TimeSpan endTime);
        Task CreateAsync(CreateTariffModel createModel);
        Task UpdateAsync(UpdateTariffModel updateModel);
        Task DeleteAsync(string tariffId);
    }
}
