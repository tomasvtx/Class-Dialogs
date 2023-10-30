using System.Windows;

namespace Dialogs
{
    /// <summary>
    /// Interakční logika pro LoginDialog.xaml
    /// </summary>
    public partial class LoginDialog : Window
    {
        public string Jmeno = string.Empty;
        public string Password = string.Empty;
        private Login loginControl;

        public LoginDialog()
        {
            InitializeComponent();

            // Vytvoření instance třídy Login a nastavení jako obsahu okna
            loginControl = new Login();
            this.Content = loginControl;
        }
    }
}
