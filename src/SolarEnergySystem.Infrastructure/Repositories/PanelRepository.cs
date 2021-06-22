using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SolarEnergySystem.Core.DTO;
using SolarEnergySystem.Core.Interfaces;

namespace SolarEnergySystem.Infrastructure.Repositories
{
    public class PanelRepository : IPanelRepository
    {
        private readonly SolarEnergySystemDatabaseContext _context;

        public PanelRepository(SolarEnergySystemDatabaseContext context)
        {
            _context = context;
        }

        public IReadOnlyList<HourlyReportDto> GetHourlyReport(string panelId)
        {
            var today = DateTime.UtcNow.Date;
            var tomorrow = DateTime.UtcNow.AddDays(1).Date;

            //retornando todas las readings , se procede a buscar el panel , se INCLUYE todas las mediciones que tiene el panel
            // pero solo las mediciones en el rango de fecha.

            var readings = _context.Panel
                .Include(p => p.ElectricityReadings).First(x => x.Id == panelId)
                .ElectricityReadings.Where(x => x.ReadingDateTime >= today && x.ReadingDateTime <= tomorrow).ToList();

            //se procede a retornar las lecturas y agruparlas por hora
            // se agrupa por la llave (hora de la fecha)

            return readings.GroupBy(x => x.ReadingDateTime.Hour, (k, g) => new HourlyReportDto
            {
                //el promedio de saca del grupo
                //el campo que se utiliza para el promedio es kw
                Average = g.Average(e => e.KiloWatt),
                Sum = g.Sum(e => e.KiloWatt),
                Max = g.Max(e => e.KiloWatt),
                Min = g.Min(e => e.KiloWatt),
                //k es la llave
                Hour = k
            }).ToList();
        }
    }
}
