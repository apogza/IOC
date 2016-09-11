using IOC.Calculations.Interfaces;
using IOC.WPF.Commands;
using IOC.WPF.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace IOC.WPF
{
    /// <summary>
    /// The view model behind the main window
    /// </summary>
    public class MainWindowViewModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindowViewModel()
        {
            CloseCommand = new RelayCommand(p => true, a => Application.Current.Shutdown(1));
            CalculateCommand = new RelayCommand(p => CanCalculationExecute(), a => Calculate());
            DialogService = ContainerTypes.Resolve<IDialogService>();

            InitCalculations();
        }

        //the commands to be used by the window
        public ICommand CloseCommand { get; private set; }
        public ICommand CalculateCommand { get; private set; }

        //a dialog service that allows us to display a message box
        //from the view model
        private IDialogService DialogService { get; set; }

        //the parameters needed by the calculation
        public double Distance { get; set; }
        public double VolumeFuelConsumed { get; set; }

        //the selected calculation
        public ICalculation Calculation { get; set; }

        //the collection of all calculation to be given as a choice
        //to the user
        public ObservableCollection<ICalculation> Calculations { get; private set; }

        /// <summary>
        /// Indicates whether the selected calculation can execute
        /// </summary>
        /// <returns>true, if it can; no, otherwise</returns>
        private bool CanCalculationExecute()
        {
            return Distance > 0 && VolumeFuelConsumed > 0 && Calculation != null;
        }

        /// <summary>
        /// Init function to obtain an instance of each calculation
        /// using the IOC container
        /// </summary>
        private void InitCalculations()
        {
            Calculations = new ObservableCollection<ICalculation>();
            Calculations.Add(ContainerTypes.Resolve<ILitersPerHunderdKms>());
            Calculations.Add(ContainerTypes.Resolve<IMilesPerGallon>());

            //init the selected calculation
            Calculation = Calculations[0];
        }

        /// <summary>
        /// A method to lauch the calculation
        /// and display the result
        /// </summary>
        private void Calculate()
        {
            double result = Calculation.Calculate(Distance, VolumeFuelConsumed);
            DialogService.ShowMessageBox(string.Format("The consumption is {0:N2} {1}", result, Calculation.ResultLabel));
        }
    }
}
