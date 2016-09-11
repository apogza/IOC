using IOC.Calculations.Interfaces;
using IOC.WPF.Commands;
using IOC.WPF.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace IOC.WPF
{
    public class MainWindowViewModel
    {
        public ICommand CloseCommand { get; private set; }
        public ICommand CalculateCommand { get; private set; }

        private IDialogService DialogService { get; set; }

        public double Distance { get; set; }
        public double VolumeFuelConsumed { get; set; }

        public ICalculation Calculation { get; set; }
        public ObservableCollection<ICalculation> Calculations { get; private set; }

        public MainWindowViewModel()
        {
            CloseCommand = new RelayCommand(p => true, a => Application.Current.Shutdown(1));
            CalculateCommand = new RelayCommand(p => CanCalculationExecute(),
                a => Calculate());
            DialogService = ContainerTypes.Resolve<IDialogService>();

            InitCalculations();
        }

        private bool CanCalculationExecute()
        {
            return Distance > 0 && VolumeFuelConsumed > 0 && Calculation != null;
        }

        private void InitCalculations()
        {
            Calculations = new ObservableCollection<ICalculation>();
            Calculations.Add(ContainerTypes.Resolve<ILitersPerHunderdKms>());
            Calculations.Add(ContainerTypes.Resolve<IMilesPerGallon>());

            Calculation = Calculations[0];
        }

        private void Calculate()
        {
            double result = Calculation.Calculate(Distance, VolumeFuelConsumed);
            DialogService.ShowMessageBox(string.Format("The consumption is {0:N2} {1}", result, Calculation.ResultLabel));
        }
    }
}
