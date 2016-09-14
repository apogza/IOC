using IOC.Calculations.Interfaces;

namespace IOC.Calculations
{
    public class LitersPerHundredKms : ILitersPerHunderdKms
    {
        private IKmVerdict _verdict;
        private double _result;

        public LitersPerHundredKms(IKmVerdict verdict)
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
            _result = fuelVolumeConsumption / distance * 100;
            return _result;
        }

        public string GetVerdict()
        {
            return _verdict.GetVerdict(_result);
        }
    }
}
