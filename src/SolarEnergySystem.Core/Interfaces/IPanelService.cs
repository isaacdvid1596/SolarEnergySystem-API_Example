using System;
using System.Collections.Generic;
using System.Text;
using SolarEnergySystem.Core.DTO;

namespace SolarEnergySystem.Core.Interfaces
{
    public interface IPanelService
    {
        //get all method
        ServiceResult<IReadOnlyList<PanelDto>> GetAllPanels();
        ServiceResult<bool> RegisterElectricityReading(string panelId,double electricityReading);
        ServiceResult<IReadOnlyList<HourlyReportDto>> GetHourlyReport(string panelId);
    }
}
