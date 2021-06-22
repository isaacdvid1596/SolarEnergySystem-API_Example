using System;
using System.Collections.Generic;
using System.Text;
using SolarEnergySystem.Core.Entities;

namespace SolarEnergySystem.Core.Rules
{
    public abstract class BaseRule
    {
        //metodo para ver si aplica la regla
        public abstract bool Applies(Panel panel);
        
        //metodo que aplica la regla
        protected abstract void ApplyPanelSpecificRules(Panel panel,ElectricityReading lastReading, double currentReading);

        //si una regla se aplica a todas usar un template
        //metodo que contenga la logica de todas las reglas
        public void ApplyRule(Panel panel,double currentElectricityReading,ElectricityReading electricityReading)
        {
            if (currentElectricityReading <= 0)
            {
                throw new ApplicationException("Incorrect Reading Value");
            }
            
            ApplyPanelSpecificRules(panel,electricityReading,currentElectricityReading);
        }
    }
}
