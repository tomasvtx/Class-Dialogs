using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;

namespace Dialogs
{
    /// <summary>
    /// Třída CustomMessageBox reprezentuje vlastní dialogové okno s konfigurovatelnými tlačítky.
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        public MessageBoxResult MessageBoxResult;

        public CustomMessageBox(string Title, string Message, MessageBoxButton messageBoxButton, string Button1 = null, string Button2 = null, string Button3 = null)
        {
            InitializeComponent();

            this.Title = Title;
            this.ShowInTaskbar = false;
            this.Content.Text = Message;

            // Konfigurace viditelnosti a textu tlačítek v závislosti na zvoleném MessageBoxButton typu.
            SetButtonVisibility(messageBoxButton);

            // Nastavení textu pro jednotlivá tlačítka.
            SetButtonText(OK, Button1);
            SetButtonText(OK_CANCEL_OK, Button1);
            SetButtonText(YES_NO_CANCEL_YES, Button1);
            SetButtonText(YES_NO_YES, Button1);

            SetButtonText(OK_CANCEL_CANCEL, Button2);
            SetButtonText(YES_NO_CANCEL_NO, Button2);
            SetButtonText(YES_NO_NO, Button2);

            SetButtonText(YES_NO_CANCEL_CANCEL, Button3);
                }

        /// <summary>
        /// Nastaví text tlačítka, pokud je poskytnutý text neprázdný.
        /// </summary>
        private void SetButtonText(Button button, string text)
        {
            if (text != null)
            {
                button.Content = text;
            }
        }

        /// <summary>
        /// Nastaví viditelnost tlačítek v závislosti na zvoleném MessageBoxButton typu.
        /// </summary>
        private void SetButtonVisibility(MessageBoxButton button)
        {
            OK.Visibility = button == MessageBoxButton.OK ? Visibility.Visible : Visibility.Collapsed;
            OK_CANCEL_OK.Visibility = button == MessageBoxButton.OKCancel ? Visibility.Visible : Visibility.Collapsed;
            OK_CANCEL_CANCEL.Visibility = button == MessageBoxButton.OKCancel ? Visibility.Visible : Visibility.Collapsed;
            YES_NO_CANCEL_YES.Visibility = button == MessageBoxButton.YesNoCancel ? Visibility.Visible : Visibility.Collapsed;
            YES_NO_CANCEL_NO.Visibility = button == MessageBoxButton.YesNoCancel ? Visibility.Visible : Visibility.Collapsed;
            YES_NO_CANCEL_CANCEL.Visibility = button == MessageBoxButton.YesNoCancel ? Visibility.Visible : Visibility.Collapsed;
            YES_NO_YES.Visibility = button == MessageBoxButton.YesNo ? Visibility.Visible : Visibility.Collapsed;
            YES_NO_NO.Visibility = button == MessageBoxButton.YesNo ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Metoda pro tlačítko "OK" - nastaví MessageBoxResult na OK a zavře dialogové okno.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonOK(object sender, RoutedEventArgs e)
        {
            MessageBoxResult = MessageBoxResult.OK;
            Close();
        }

        /// <summary>
        /// Metoda pro tlačítko "Cancel" - nastaví MessageBoxResult na Cancel a zavře dialogové okno.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton(object sender, RoutedEventArgs e)
        {
            MessageBoxResult = MessageBoxResult.Cancel;
            Close();
        }

        /// <summary>
        /// Metoda pro tlačítko "Yes" - nastaví MessageBoxResult na Yes a zavře dialogové okno.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void YesButton(object sender, RoutedEventArgs e)
        {
            MessageBoxResult = MessageBoxResult.Yes;
            Close();
        }

        /// <summary>
        /// Metoda pro tlačítko "No" - nastaví MessageBoxResult na No a zavře dialogové okno.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NoButton(object sender, RoutedEventArgs e)
        {
            MessageBoxResult = MessageBoxResult.No;
            Close();
        }
    }
}
