using IOC.Calculations.Interfaces;

namespace IOC.Calculations
{
    public class KmVerdict : IKmVerdict
    {
        public string GetVerdict(double fuelConsumption)
        {
            return (fuelConsumption > 7.5) ? "Get a new car!" : "Not bad!";
        }
    }
}
