using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SolarEnergySystem.Core.Entities;
using SolarEnergySystem.Core.Enums;
using SolarEnergySystem.Infrastructure;
using Xunit;

namespace SolarEnergySystem.Tests
{

    //todo esto es prepwork para tener una base de datos en memoria por motivos de testing
    public static class DbContextUtils
    {
        public static SolarEnergySystemDatabaseContext GetInMemoryContext()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var dbContextOptions = new DbContextOptionsBuilder<SolarEnergySystemDatabaseContext>()
                .UseSqlite(connection)
                .Options;

            var context = new SolarEnergySystemDatabaseContext(dbContextOptions);
            context.Database.EnsureCreated();
            return context;
        }

        public static void SeedData(this SolarEnergySystemDatabaseContext context)
        {

            //copy paste todo el seeding del DbContextExtensions.cs

            context.AddRange(new List<Panel>
            {
                new Panel
                {
                    Brand = "Brand1",
                    Latitude = -3850.04,
                    Longitude = 450.93,
                    MeasuringUnit = MeasuringUnit.KiloWatt,
                    PanelType = PanelType.Regular,
                    Id = "A305V5",
                    ElectricityReadings = new List<ElectricityReading>
                    {
                        new ElectricityReading
                        {
                            KiloWatt = 560,
                            ReadingDateTime = DateTime.UtcNow
                        },new ElectricityReading
                        {
                            KiloWatt = 220,
                            ReadingDateTime = DateTime.UtcNow.AddHours(-1)
                        },
                        new ElectricityReading
                        {
                            KiloWatt = 609,
                            ReadingDateTime = DateTime.UtcNow.AddHours(-1)
                        },
                        new ElectricityReading
                        {
                            KiloWatt = 560,
                            ReadingDateTime = DateTime.UtcNow.AddHours(-2)
                        },
                        new ElectricityReading
                        {
                            KiloWatt = 345,
                            ReadingDateTime = DateTime.UtcNow.AddHours(-2)
                        }
                    }
                },
                new Panel
                {
                    Brand = "Brand2",
                    Latitude = -3867.04,
                    Longitude = -4607.92,
                    MeasuringUnit = MeasuringUnit.Watt,
                    PanelType = PanelType.Limited,
                    Id = "BU492K",
                    ElectricityReadings = new List<ElectricityReading>
                    {
                        new ElectricityReading
                        {
                            KiloWatt = 6707,
                            ReadingDateTime = DateTime.UtcNow
                        },
                        new ElectricityReading
                        {
                            KiloWatt = 5670,
                            ReadingDateTime = DateTime.UtcNow.AddDays(-1)
                        },
                        new ElectricityReading
                        {
                            KiloWatt = 2450,
                            ReadingDateTime = DateTime.UtcNow.AddDays(-2)
                        }
                    }
                },
                new Panel
                {
                    Brand = "Brand3",
                    Latitude = 578.0,
                    Longitude = -245.5,
                    MeasuringUnit = MeasuringUnit.Watt,
                    PanelType = PanelType.Ultimate,
                    Id = "CFJ39R",
                    ElectricityReadings = new List<ElectricityReading>
                    {
                        new ElectricityReading
                        {
                            KiloWatt = 6707,
                            ReadingDateTime = DateTime.UtcNow
                        },
                        new ElectricityReading
                        {
                            KiloWatt = 5670,
                            ReadingDateTime = DateTime.UtcNow.AddMinutes(-1)
                        },
                        new ElectricityReading
                        {
                            KiloWatt = 2450,
                            ReadingDateTime = DateTime.UtcNow.AddMinutes(-2)
                        }
                    }
                }
            });

            context.SaveChanges();

        }
    }
}
