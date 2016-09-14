using IOC.Calculations.Interfaces;

namespace IOC.Calculations
{
    public class KmVerdict : IKmVerdict
    {
        public string GetVerdict(double fuelConsumption)
        {
            if(fuelConsumption > 7.5)
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
