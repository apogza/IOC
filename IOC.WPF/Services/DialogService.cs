using IOC.WPF.Services.Interfaces;
using System.Windows;

namespace IOC.WPF.Services
{
    public class DialogService : IDialogService
    {
        /// <summary>
        /// Show a message box
        /// </summary>
        /// <param name="message">the message to be shown on screen</param>
        public void ShowMessageBox(string message)
        {
            MessageBox.Show(message);
        }
    }
}
