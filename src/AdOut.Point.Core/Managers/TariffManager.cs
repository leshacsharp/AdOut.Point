using AdOut.Point.Model.Api;
using AdOut.Point.Model.Classes;
using AdOut.Point.Model.Database;
using AdOut.Point.Model.Exceptions;
using AdOut.Point.Model.Interfaces.Managers;
using AdOut.Point.Model.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using static AdOut.Point.Model.Constants;

namespace AdOut.Point.Core.Managers
{
    public class TariffManager : BaseManager<Tariff>, ITariffManager
    {
        private readonly ITariffRepository _tariffRepository;
        private readonly IAdPointRepository _adPointRepository;

        public TariffManager(
            ITariffRepository tariffRepository,
            IAdPointRepository adPointRepository)
            : base(tariffRepository)
        {
            _tariffRepository = tariffRepository;
            _adPointRepository = adPointRepository;
        }

        public async Task<ValidationResult<string>> ValidateTariff(int adPointId, TimeSpan startTime, TimeSpan endTime)
        {
            var adPoint = await _adPointRepository.GetByIdAsync(adPointId);
            if (adPoint == null)
            {
                throw new ObjectNotFoundException($"AdPoint with id={adPointId} was not found");
            }

            var validationResult = new ValidationResult<string>();

            if (startTime < adPoint.StartWorkingTime || endTime > adPoint.EndWorkingTime)
            {
                var adPointWorkingTime = $"{adPoint.StartWorkingTime} - {adPoint.EndWorkingTime}";
                var tariffTimeBounds = $"{startTime} - {endTime}";
                var validationMessage = string.Format(TariffValidationMessages.TimeLeavsOfBounds, tariffTimeBounds, adPointWorkingTime);

                validationResult.Errors.Add(validationMessage);
            }

            var haveTimeIntersection = await _tariffRepository.Read(t =>
                           (startTime <= t.StartTime && endTime >= t.StartTime ||
                            startTime <= t.EndTime && endTime >= t.EndTime ||
                            startTime >= t.StartTime && endTime <= t.EndTime) &&
                            t.AdPointId == adPoint.Id).AnyAsync();

            if (haveTimeIntersection)
            {
                var tariffTimeBounds = $"{startTime} - {endTime}";
                var validationMessage = string.Format(TariffValidationMessages.TimeInteresection, tariffTimeBounds);

                validationResult.Errors.Add(validationMessage);
            }

            return validationResult;
        }

        public async Task CreateAsync(CreateTariffModel createModel)
        {
            if (createModel == null)
            {
                throw new ArgumentNullException(nameof(createModel));
            }

            var adPoint = await _adPointRepository.GetByIdAsync(createModel.AdPointId);
            if (adPoint == null)
            {
                throw new ObjectNotFoundException($"AdPoint with id={createModel.AdPointId} was not found");
            }

            var tariff = new Tariff()
            {
                AdPoint = adPoint,
                StartTime = createModel.StartTime,
                EndTime = createModel.EndTime,
                PriceForMin = createModel.PriceForMin
            };

            Create(tariff);
        }

        public async Task UpdateAsync(UpdateTariffModel updateModel)
        {
            if (updateModel == null)
            {
                throw new ArgumentNullException(nameof(updateModel));
            }

            var tariff = await _tariffRepository.GetByIdAsync(updateModel.TariffId);
            if (tariff == null)
            {
                throw new ObjectNotFoundException($"Tariff with id={updateModel.TariffId} was not found");
            }

            tariff.PriceForMin = updateModel.PriceForMin;
            tariff.StartTime = updateModel.StartTime;
            tariff.EndTime = updateModel.EndTime;

            Update(tariff);
        }

        public async Task DeleteAsync(int tariffId)
        {
            var tariff = await _tariffRepository.GetByIdAsync(tariffId);
            if (tariff == null)
            {
                throw new ObjectNotFoundException($"Tariff with id={tariffId} was not found");
            }

            Delete(tariff);
        }
    }
}
