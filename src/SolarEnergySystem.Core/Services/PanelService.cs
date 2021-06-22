using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolarEnergySystem.Core.DTO;
using SolarEnergySystem.Core.Entities;
using SolarEnergySystem.Core.Enums;
using SolarEnergySystem.Core.Interfaces;
using SolarEnergySystem.Core.Rules;

namespace SolarEnergySystem.Core.Services
{
    public class PanelService : IPanelService
    {
        private readonly IRepository<Panel, string> _panelBasicRepository;
        //specific repo
        private readonly IElectricityReadingRepository _electricityReadingRepository;
        private readonly IRepository<ElectricityReading, int> _electricityRepository;
        private readonly IPanelRepository _panelRepository;
        private readonly IEnumerable<BaseRule> _rules;


        public PanelService(IRepository<Panel,string> panelBasicBasicRepository,
            IElectricityReadingRepository electricityReadingRepository,
            IRepository<ElectricityReading,int> electricityRepository,
            IPanelRepository panelRepository,
            IEnumerable<BaseRule> rules)
        {
            _panelBasicRepository = panelBasicBasicRepository;
            _electricityReadingRepository = electricityReadingRepository;
            _electricityRepository = electricityRepository;
            _panelRepository = panelRepository;
            _rules = rules;
        }

        public ServiceResult<IReadOnlyList<PanelDto>> GetAllPanels()
        { 
            //tiene que retornar todos los paneles existentes
            //ordenado por tipo de panel

            var panels = _panelBasicRepository.GetAll().Select(x => new PanelDto
            {
                Brand = x.Brand,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                MeasuringUnit = x.MeasuringUnit,
                PanelType = x.PanelType,
                SerialNumber = x.Id
            }).OrderBy(x => x.PanelType);

            return ServiceResult<IReadOnlyList<PanelDto>>.SuccessResult(panels.ToList());
        }

        public ServiceResult<bool> RegisterElectricityReading(string panelId, double electricityReading)
        {
            var panel = _panelBasicRepository.GetById(panelId);

            if (panel == null)
            {
                return ServiceResult<bool>.ErrorResult($"Panel with id {panelId} was not found");
            }

            var lastReading = _electricityReadingRepository.GetMostRecentReading(panelId);

            electricityReading = ConvertUnits(electricityReading, panel);

            ApplyRules(panel, electricityReading, lastReading);

            var newReading = new ElectricityReading
            {
                ReadingDateTime = DateTime.UtcNow,
                KiloWatt = electricityReading,
                PanelId = panelId
            };

            _electricityRepository.Add(newReading);
            return ServiceResult<bool>.SuccessResult(true);
        }

        public ServiceResult<IReadOnlyList<HourlyReportDto>> GetHourlyReport(string panelId)
        {
            var panel = _panelBasicRepository.GetById(panelId);
            if (panel == null)
            {
                return ServiceResult<IReadOnlyList<HourlyReportDto>>.NotFoundResult("");
            }

            var report = _panelRepository.GetHourlyReport(panelId);
            return ServiceResult<IReadOnlyList<HourlyReportDto>>.SuccessResult(report);
        }

        private void ApplyRules(Panel panel, double electricityReading, ElectricityReading lastReading)
        {
            foreach (var rule in _rules)
            {
                if (rule.Applies(panel))
                {
                    rule.ApplyRule(panel,electricityReading,lastReading);
                    break;
                }
            }
        }

        private double ConvertUnits(double electricityReading, Panel panel)
        {
            return panel.MeasuringUnit == MeasuringUnit.KiloWatt ? electricityReading : electricityReading / 1000;
        }
    }
}
