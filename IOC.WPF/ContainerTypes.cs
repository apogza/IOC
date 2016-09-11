using System.Windows;
using IOC.Calculations;
using IOC.Calculations.Interfaces;
using IOC.WPF.Services.Interfaces;
using IOC.WPF.Services;
namespace IOC.WPF
{
    public class ContainerTypes
    {
        public ContainerTypes()
        {
            RegisterComponents();
        }

        private Container Container
        {
            get
            {
                return ((App)Application.Current).Container;
            }
        }

        /// <summary>
        /// List and register all the components
        /// </summary>
        private void RegisterComponents()
        {
            Container.RegisterType<MainWindowViewModel>();
            Container.RegisterType<ILitersPerHunderdKms, LitersPerHundredKms>(true);
            Container.RegisterType<IMilesPerGallon, MilesPerGallon>(true);
            Container.RegisterType<IDialogService, DialogService>(true);
        }

        /// <summary>
        /// An easier way to access the IOC container
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Resolve<T>()
        {
            return ((App)Application.Current).Container.Resolve<T>();
        }


    }
}
