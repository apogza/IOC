using IOC.WPF.Services.Interfaces;
using System.Windows;

namespace IOC.WPF.Services
{
    public class DialogService : IDialogService
    {
        public void ShowMessageBox(string message)
        {
            MessageBox.Show(message);
        }
    }
}
