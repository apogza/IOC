using System.Windows;
using IOC.Calculations;
using IOC.Calculations.Interfaces;
using IOC.WPF.Services.Interfaces;
using IOC.WPF.Services;
using System.Collections.Generic;

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
            
            Container.RegisterType<IKmVerdict, KmVerdict>(true);
            Container.RegisterType<IMilesVerdict, MilesVerdict>(true);
            Container.RegisterType<ICalculation, MilesPerGallon>(true);
            Container.RegisterType<ICalculation, LitersPerHundredKms>(true);
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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> ResolveList<T>()
        {
            return ((App)Application.Current).Container.ResolveList<T>();
        }
    }
}
