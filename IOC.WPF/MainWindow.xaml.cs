using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace IOC.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = ContainerTypes.Resolve<MainWindowViewModel>();
        }

        /// <summary>
        /// Used to make sure that we enter only numerical values
        /// for distance and fuel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
