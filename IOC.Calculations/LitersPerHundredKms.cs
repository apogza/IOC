using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOC.Calculations.Interfaces;

namespace IOC.Calculations
{
    public class LitersPerHundredKms : ILitersPerHunderdKms
    {
        public string CalculationLabel
        {
            get
            {
                return "Liters per 100 kms";
            }
        }

        public string DistanceLabel
        {
            get
            {
                return "Kilometers:";
            }
        }

        public string FuelVolumeLabel
        {
            get
            {
                return "Liters:";
            }
        }

        public string ResultLabel
        {
            get
            {
                return "liters per 100 kms";
            }
        }

        public double Calculate(double distance, double fuelVolumeConsumption)
        {
            return fuelVolumeConsumption / distance * 100;
        }
    }
}
