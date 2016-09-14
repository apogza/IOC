using IOC.Calculations.Interfaces;

namespace IOC.Calculations
{
    public class MilesPerGallon : IMilesPerGallon
    {
        private IMilesVerdict _verdict;
        private double _result;

        public MilesPerGallon(IMilesVerdict verdict)
        {
            _verdict = verdict;
        }

        public IVerdict Verdict
        {
            get { return _verdict; }
        }


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
            _result = distance / fuelVolumeConsumption;
            return _result;
        }

        public string GetVerdict()
        {
            return _verdict.GetVerdict(_result);
        }
    }
}
