using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolarEnergySystem.Infrastructure.Repositories;
using Xunit;

namespace SolarEnergySystem.Tests
{
    public class PanelRepositoryTests
    {
        [Fact]
        public void GetHourlyReport_ValidPanelId_ReturnsCorrectMeanSumMaxAndMin()
        {
            //chequear dependencias del metodo og
            //dependencia del contexto


            //ARRANGE
            var context = DbContextUtils.GetInMemoryContext();
            context.SeedData();
            var repository = new PanelRepository(context);

            var today = DateTime.UtcNow.Date;
            var tomorrow = DateTime.UtcNow.AddDays(1).Date;

            //ACT
            var result = repository.GetHourlyReport("A305V5");

            //ASSERT
            //usar este cuando es una lista
            Assert.NotEmpty(result);
            Assert.Contains(result, x=> x.Hour == DateTime.UtcNow.Hour);


            foreach (var hourlyReportDto in result)
            {
                var matchingResult = context.ElectricityReading
                    .Where(x => x.PanelId == "A305V5" && x.ReadingDateTime >= today && x.ReadingDateTime <= tomorrow);

                Assert.Equal(matchingResult.Sum(x=>x.KiloWatt),hourlyReportDto.Sum);
                Assert.Equal(matchingResult.Average(x=>x.KiloWatt),hourlyReportDto.Average);
                Assert.Equal(matchingResult.Max(x=>x.KiloWatt),hourlyReportDto.Max);
                Assert.Equal(matchingResult.Min(x=>x.KiloWatt),hourlyReportDto.Min);
            }
        }
    }
}
