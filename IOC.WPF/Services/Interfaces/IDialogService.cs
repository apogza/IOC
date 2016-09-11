using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.WPF.Services.Interfaces
{
    /// <summary>
    /// An interface for a dialog service
    /// whose purpose is to display a message box
    /// on the screen. To be used in a view model.
    /// </summary>
    interface IDialogService
    {
        /// <summary>
        /// Show a message on the screen
        /// </summary>
        /// <param name="message">The message to be shown</param>
        void ShowMessageBox(string message);
    }
}
