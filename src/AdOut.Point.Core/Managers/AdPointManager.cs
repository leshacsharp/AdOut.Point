﻿using AdOut.Point.Model.Api;
using AdOut.Point.Model.Database;
using AdOut.Point.Model.Dto;
using AdOut.Point.Model.Exceptions;
using AdOut.Point.Model.Interfaces.Managers;
using AdOut.Point.Model.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdOut.Point.Core.Managers
{
    public class AdPointManager : BaseManager<AdPoint>, IAdPointManager
    {
        private readonly IAdPointRepository _adPointRepository;
        private readonly ITariffRepository _tariffRepository;
        private readonly IAdPointDayOffRepository _adPointDayOffRepository;

        public AdPointManager(
            IAdPointRepository adPointRepository,
            ITariffRepository tariffRepository,
            IAdPointDayOffRepository adPointDayOffRepository) 
            : base(adPointRepository)
        {
            _adPointRepository = adPointRepository;
            _tariffRepository = tariffRepository;
            _adPointDayOffRepository = adPointDayOffRepository;
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
   
            //todo: make proccess of adding images
        }

        public Task<AdPointDto> GetByIdAsync(int adPointId)
        { 
            return _adPointRepository.GetDtoByIdAsync(adPointId);
        }
    }
}
