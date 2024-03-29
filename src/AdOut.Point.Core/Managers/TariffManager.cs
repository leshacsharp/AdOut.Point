﻿using AdOut.Extensions.Exceptions;
using AdOut.Point.Model.Api;
using AdOut.Point.Model.Classes;
using AdOut.Point.Model.Database;
using AdOut.Point.Model.Interfaces.Managers;
using AdOut.Point.Model.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using static AdOut.Point.Model.Constants;

namespace AdOut.Point.Core.Managers
{
    public class TariffManager : ITariffManager
    {
        private readonly ITariffRepository _tariffRepository;
        private readonly IAdPointRepository _adPointRepository;

        public TariffManager(
            ITariffRepository tariffRepository,
            IAdPointRepository adPointRepository)
        {
            _tariffRepository = tariffRepository;
            _adPointRepository = adPointRepository;
        }

        public async Task<ValidationResult<string>> ValidateTariff(string adPointId, TimeSpan startTime, TimeSpan endTime)
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
                var validationMessage = string.Format(ValidationMessages.TariffTimeLeavsOfBounds, tariffTimeBounds, adPointWorkingTime);
                validationResult.Errors.Add(validationMessage);
            }

            var haveTimeIntersection = await _tariffRepository.Read(t =>
                               t.AdPointId == adPoint.Id &&
                               startTime < t.EndTime &&
                               t.StartTime < endTime ).AnyAsync();

            if (haveTimeIntersection)
            {
                var tariffTimeBounds = $"{startTime} - {endTime}";
                var validationMessage = string.Format(ValidationMessages.TariffTimeInteresection, tariffTimeBounds);
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
                PriceForMinute = createModel.PriceForMinute
            };

            _tariffRepository.Create(tariff);
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

            tariff.PriceForMinute = updateModel.PriceForMinute;
            tariff.StartTime = updateModel.StartTime;
            tariff.EndTime = updateModel.EndTime;

            _tariffRepository.Update(tariff);
        }

        public async Task DeleteAsync(string tariffId)
        {
            var tariff = await _tariffRepository.GetByIdAsync(tariffId);
            if (tariff == null)
            {
                throw new ObjectNotFoundException($"Tariff with id={tariffId} was not found");
            }

            _tariffRepository.Delete(tariff);
        }
    }
}
