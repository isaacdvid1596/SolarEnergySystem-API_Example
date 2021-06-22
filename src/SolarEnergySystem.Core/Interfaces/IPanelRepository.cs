using System;
using System.Collections.Generic;
using System.Text;
using SolarEnergySystem.Core.DTO;

namespace SolarEnergySystem.Core.Interfaces
{
    public interface IPanelRepository
    {
        IReadOnlyList<HourlyReportDto> GetHourlyReport(string panelId);
    }
}
