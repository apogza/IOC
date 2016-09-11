namespace IOC.Calculations.Interfaces
{
    public interface ICalculation
    {
        /// <summary>
        /// Labels to be used in the UI
        /// </summary>
        string CalculationLabel { get; }
        string DistanceLabel { get; }
        string FuelVolumeLabel { get; }
        string ResultLabel { get; }

        /// <summary>
        /// Each ICalculation should implement a method
        /// </summary>
        /// <param name="distance">the distance</param>
        /// <param name="fuelVolumeConsumption">the fuel volume consumed</param>
        /// <returns></returns>
        double Calculate(double distance, double fuelVolumeConsumption);
    }
}
