using System;
using System.Collections.Generic;
using System.Text;
using SolarEnergySystem.Core.Entities;
using SolarEnergySystem.Core.Enums;

namespace SolarEnergySystem.Core.Rules
{
    public class LimitedPanelRule : BaseRule
    {
        public override bool Applies(Panel panel)
        {
            return panel.PanelType == PanelType.Limited;
        }

        protected override void ApplyPanelSpecificRules(Panel panel, ElectricityReading lastReading, double currentReading)
        {
            if ((DateTime.UtcNow - lastReading.ReadingDateTime).Days < 1)
            {
                throw new ApplicationException("Reading cannot be registered");
            }

            if (currentReading < 5)
            {
                throw new ApplicationException("Reading value too low");
            }
        }
    }
}
