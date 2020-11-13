using System.Collections.Generic;

namespace AdOut.Point.Model.Dto
{
    public class PlanTariffDto
    {
        public string AdPointId { get; set; }

        public List<TariffDto> Tariffs { get; set; }
    }
}
