using System.Windows;
using System.Windows.Controls;

namespace Dialogs
{
    /// <summary>
    /// Tato třída obsahuje interakční logiku pro Login.xaml UserControl.
    /// </summary>
    public partial class Login : UserControl
    {
        // Proměnné pro uchování jména a hesla
        public string Jmeno = string.Empty;
        public string Password = string.Empty;

        /// <summary>
        /// Konstruktor třídy Login, inicializuje UserControl.
        /// </summary>
        public Login()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Obsluha události tlačítka "Přihlásit". Získá jméno a heslo z TextBoxů, předá je nadřazenému oknu LoginDialog
        /// a zavře nadřazené okno.
        /// </summary>
        private void _Login(object sender, RoutedEventArgs e)
        {
            // Získání jména a hesla z TextBoxů
            Jmeno = jmeno.Text;
            Password = password.Password;

            // Získání nadřazeného okna typu LoginDialog
            LoginDialog parentWindow = (LoginDialog)Window.GetWindow(this);

            if (parentWindow != null)
            {
                // Předání jména a hesla nadřazenému oknu
                parentWindow.Jmeno = Jmeno;
                parentWindow.Password = Password;

                // Zavření nadřazeného okna
                parentWindow.Close();
            }
        }
    }
}
