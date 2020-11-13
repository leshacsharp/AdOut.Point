using AdOut.Point.Model.Api;
using AdOut.Point.Model.Database;
using AdOut.Point.Model.Dto;
using AdOut.Point.Model.Exceptions;
using AdOut.Point.Model.Interfaces.Managers;
using AdOut.Point.Model.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdOut.Point.Core.Managers
{
    public class AdPointManager : BaseManager<AdPoint>, IAdPointManager
    {
        private readonly IAdPointRepository _adPointRepository;
        private readonly ITariffRepository _tariffRepository;
        private readonly IAdPointDayOffRepository _adPointDayOffRepository;
        private readonly IPlanAdPointRepository _planAdPointRepository;

        public AdPointManager(
            IAdPointRepository adPointRepository,
            ITariffRepository tariffRepository,
            IAdPointDayOffRepository adPointDayOffRepository,
            IPlanAdPointRepository planAdPointRepository) 
            : base(adPointRepository)
        {
            _adPointRepository = adPointRepository;
            _tariffRepository = tariffRepository;
            _adPointDayOffRepository = adPointDayOffRepository;
            _planAdPointRepository = planAdPointRepository;
        }

        public void Create(CreateAdPointModel createModel, string userId)
        {
            if (createModel == null)
            {
                throw new ArgumentNullException(nameof(createModel));
            }

            var adPoint = new AdPoint()
            {
                UserId = userId,
                Location = createModel.Location,
                StartWorkingTime = createModel.StartWorkingTime,
                EndWorkingTime = createModel.EndWorkingTime,
                ScreenWidthCm = createModel.ScreenWidthCm,
                ScreenHeightCm = createModel.ScreenHeightCm
            };

            Create(adPoint);
        }

        public async Task DeleteAsync(string adPointId)
        {
            var adPoint = await _adPointRepository.GetByIdAsync(adPointId);
            if (adPoint == null)
            {
                throw new ObjectNotFoundException($"AdPoint with id={adPointId} was not found");
            }

            var adPointHasPlans = await _planAdPointRepository.Read(pap => pap.AdPointId == adPointId).AnyAsync();
            if (adPointHasPlans)
            {
                throw new InvalidOperationException($"AdPoint with id={adPointId} has plans. AdPoint can't be deleted");
            }

            Delete(adPoint);
        }

        public Task<AdPointDto> GetByIdAsync(string adPointId)
        { 
            return _adPointRepository.GetDtoByIdAsync(adPointId);
        }
    }
}
