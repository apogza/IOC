using System.Windows;

namespace IOC.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        
        public App()
        {
            Container = new Container();
            RegisterComponents();
        }

        public Container Container { get; set; }

        private void RegisterComponents()
        {
            new ContainerTypes();
        }
    }
}
