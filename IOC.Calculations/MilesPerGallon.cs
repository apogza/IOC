using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOC.Calculations.Interfaces;

namespace IOC.Calculations
{
    public class MilesPerGallon : IMilesPerGallon
    {
        public string CalculationLabel
        {
            get
            {
                return "Miles per gallon";
            }
        }

        public string DistanceLabel
        {
            get
            {
                return "Miles:";
            }
        }

        public string FuelVolumeLabel
        {
            get
            {
                return "Gallons:";
            }
        }

        public string ResultLabel
        {
            get
            {
                return "miles per gallon";
            }
        }

        public double Calculate(double distance, double fuelVolumeConsumption)
        {
            return distance / fuelVolumeConsumption;
        }
    }
}
