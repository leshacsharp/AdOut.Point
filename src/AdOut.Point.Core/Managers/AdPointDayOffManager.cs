using AdOut.Extensions.Exceptions;
using AdOut.Point.Model.Database;
using AdOut.Point.Model.Interfaces.Managers;
using AdOut.Point.Model.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AdOut.Point.Core.Managers
{
    public class AdPointDayOffManager : IAdPointDayOffManager
    {
        private readonly IAdPointDayOffRepository _adPointDayOffRepository;
        private readonly IAdPointRepository _adPointRepository;
        private readonly IDayOffRepository _dayOffRepository;

        public AdPointDayOffManager(
            IAdPointDayOffRepository adPointDayOffRepository,
            IAdPointRepository adPointRepository,
            IDayOffRepository dayOffRepository)
        {
            _adPointDayOffRepository = adPointDayOffRepository;
            _adPointRepository = adPointRepository;
            _dayOffRepository = dayOffRepository;
        }

        public async Task AddDayOffToAdPointAsync(string adPointId, string dayOffId)
        {
            var adPoint = await _adPointRepository.GetByIdAsync(adPointId);
            if (adPoint == null)
            {
                throw new ObjectNotFoundException($"AdPoint with id={adPointId} was not found");
            }

            var dayOff = await _dayOffRepository.GetByIdAsync(dayOffId);
            if (dayOff == null)
            {
                throw new ObjectNotFoundException($"DayOff with id={dayOffId} was not found");
            }

            var adPointHasSameDayOff = await _adPointDayOffRepository.Read(apd => apd.AdPointId == adPointId && apd.DayOffId == dayOffId).AnyAsync();
            if (adPointHasSameDayOff)
            {
                throw new BadRequestException($"AdPoints can't contain same days off");
            }

            var adPointDayOff = new AdPointDayOff()
            {
                AdPoint = adPoint,
                DayOff = dayOff
            };

            _adPointDayOffRepository.Create(adPointDayOff);
        }

        public async Task DeleteDayOffFromAdPointAsync(string adPointId, string dayOffId)
        {
            var adPointDayOff = await _adPointDayOffRepository.GetByIdAsync(adPointId, dayOffId);
            if (adPointDayOff == null)
            {
                throw new ObjectNotFoundException($"DayOff with id=(adPointId={adPointId},dayOffId={dayOffId}) was not found");
            }

            _adPointDayOffRepository.Delete(adPointDayOff);
        }
    }
}
