using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;

namespace Dialogs
{
    /// <summary>
    /// Třída CustomMessageBox reprezentuje vlastní dialogové okno s konfigurovatelnými tlačítky.
    /// </summary>
    public partial class CustomizedMessageBox : Window
    {
        public byte MessageBoxID;

        public CustomizedMessageBox(string Title, string Message, byte CountButtons, string Button1 = null, string Button2 = null, string Button3 = null, string Button4 = null)
        {
            InitializeComponent();

            this.Title = Title;
            this.ShowInTaskbar = false;
            this.Content.Text = Message;
            this.WindowStyle = WindowStyle.None;

            // Konfigurace viditelnosti a textu tlačítek v závislosti na zvoleném MessageBoxButton typu.
            SetButtonVisibility(CountButtons);

            // Nastavení textu pro jednotlivá tlačítka.
            SetButtonText(BUTTON1, Button1);
            SetButtonText(BUTTON2, Button2);
            SetButtonText(BUTTON3, Button3);
            SetButtonText(BUTTON4, Button4);
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
        private void SetButtonVisibility(byte count)
        {
            BUTTON1.Visibility = count > 0 ? Visibility.Visible : Visibility.Collapsed;
            BUTTON2.Visibility = count > 1 ? Visibility.Visible : Visibility.Collapsed;
            BUTTON3.Visibility = count > 2 ? Visibility.Visible : Visibility.Collapsed;
            BUTTON4.Visibility = count > 3 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void BUTTON1Click(object sender, RoutedEventArgs e)
        {
            MessageBoxID = 1;
            Close();
        }
        private void BUTTON2Click(object sender, RoutedEventArgs e)
        {
            MessageBoxID = 2;
            Close();
        }
        private void BUTTON3Click(object sender, RoutedEventArgs e)
        {
            MessageBoxID = 3;
            Close();
        }
        private void BUTTON4Click(object sender, RoutedEventArgs e)
        {
            MessageBoxID = 3;
            Close();
        }
    }
}
