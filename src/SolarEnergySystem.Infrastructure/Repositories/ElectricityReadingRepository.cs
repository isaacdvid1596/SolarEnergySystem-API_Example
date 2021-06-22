using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolarEnergySystem.Core.Entities;
using SolarEnergySystem.Core.Interfaces;

namespace SolarEnergySystem.Infrastructure.Repositories
{
    public class ElectricityReadingRepository : IElectricityReadingRepository
    {
        private readonly SolarEnergySystemDatabaseContext _context;

        public ElectricityReadingRepository(SolarEnergySystemDatabaseContext context)
        {
            _context = context;
        }
        public ElectricityReading GetMostRecentReading(string panelId)
        {
            return _context.ElectricityReading
                .Where(r => r.PanelId == panelId)
                .OrderByDescending(r => r.ReadingDateTime)
                .First();
        }
    }
}
