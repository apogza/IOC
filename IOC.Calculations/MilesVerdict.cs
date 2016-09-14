using IOC.Calculations.Interfaces;

namespace IOC.Calculations
{
    public class MilesVerdict : IMilesVerdict
    {
        public string GetVerdict(double fuelConsumption)
        {
            return (fuelConsumption < 25) ? "Get a new car!" : "Not bad!";
        }
    }
}
