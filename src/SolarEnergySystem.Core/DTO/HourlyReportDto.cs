using System;
using System.Collections.Generic;
using System.Text;

namespace SolarEnergySystem.Core.DTO
{
    public class HourlyReportDto
    {
        public int Hour { get; set; }
        public double Sum { get; set; }
        public double Average { get; set; }
        public double Max { get; set; }
        public double Min { get; set; }

    }
}
