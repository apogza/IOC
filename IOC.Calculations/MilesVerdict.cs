using IOC.Calculations.Interfaces;

namespace IOC.Calculations
{
    public class MilesVerdict : IMilesVerdict
    {
        public string GetVerdict(double fuelConsumption)
        {
            if(fuelConsumption < 25)
            {
                return "Get a new car!";
            }
            else
            {
                return "Not bad!";
            }
        }
    }
}
