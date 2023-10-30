using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace Dialogs
{
    /// <summary>
    /// Interakční logika pro ErrorDialog.xaml
    /// </summary>
    public partial class ErrorDialog : Window
    {
        public ViewModel.ViewModel ViewModel { get; set; } = new ViewModel.ViewModel();
        public bool IsRequestToAppClose { get; set; } = false;

        // Konstruktor třídy ErrorDialog
        public ErrorDialog(string Title, Description _description, string Text_conf, string Exception_, TypeMessage typeMessage, string appName = null)
        {
            try
            {
                // Nastavíme DataContext na instanci ViewModel
                DataContext = ViewModel;
                InitializeComponent();
            }
            catch
            {
                return; // Pokud selže inicializace, ukončíme konstruktor.
            }

            try
            {
                ViewModel.AppName = appName;

                // Vytvoříme instanci Modelu pro dialog
                var Dialog = new ViewModel.Model
                {
                    Title = Title,
                    Content = Text_conf,
                    Exception = Exception_,
                    Description = _description,
                    TypeMessage = typeMessage
                };

                // Nastavení instrukce v závislosti na typu popisu chyby
                switch (_description)
                {
                    case Description.MainDbError:
                        Dialog.Instruction = "SQL QUERY";
                        break;
                    case Description.SerialPortError:
                        Dialog.Instruction = "SERIAL PORT CONFIGURATION";
                        break;
                    case Description.ConfigurationError:
                        Dialog.Instruction = "CONF. NAME";
                        break;
                    case Description.ProgramWarning:
                        Dialog.Instruction = "APP. DETAIL";
                        break;
                    case Description.AbnormalData:
                        Dialog.Instruction = "ZKONTROLUJTE A NAČTĚTE KÓD";
                        break;
                    case Description.PlcError:
                        Dialog.Instruction = "PROBLÉM ZAŘÍZENÍ";
                        break;
                    case Description.PLSqlTransactionRollback:
                        Dialog.Instruction = "PROBLÉM UKLÁDÁNÍ DB";
                        break;
                    case Description.PrinterError:
                        Dialog.Instruction = "PROBLÉM PŘI TISKU";
                        break;
                    case Description.SemaphoreError:
                        Dialog.Instruction = "PROBLÉM S VLÁNKY APLIKACE";
                        break;
                    default:
                        break;
                        // Pro další Description není instrukce nastavena.
                }

                // Nastavení dialogu ve ViewModel
                ViewModel.Dialog = Dialog;

                // Pokud je typ zprávy Information, nastavíme obsluhu Loaded události
                if (ViewModel.Dialog.TypeMessage == TypeMessage.Information)
                {
                    Loaded += ErrorDialog_Loaded;
                }
            }
            catch (Exception dd)
            {
                MessageBox.Show(dd.Message);
            }

            // Zakážeme tlačítko zavření okna
            SystemMenuManipulator.DisableExitButton(new WindowInteropHelper(this).Handle);
        }


        /// <summary>
        /// Obsluha události nahrání (Loaded) dialogového okna chyby.
        /// </summary>
        /// <param name="sender">Odesílatel události</param>
        /// <param name="e">Argumenty události</param>
        private async void ErrorDialog_Loaded(object sender, RoutedEventArgs e)
        {
            // Čekáme 20 sekund (20000 ms) před uzavřením dialogového okna.
            await Task.Delay(20000);

            // Po uplynutí 20 sekund uzavřeme dialogové okno.
            Close();
        }

        /// <summary>
        /// Výčtový typ reprezentující různé popisy chyb a událostí.
        /// </summary>
        public enum Description
        {
            PLSqlTransactionRollback, // Chyba při rollbacku transakce PL/SQL
            MainDbError,              // Chyba hlavní databáze
            PrinterError,             // Chyba tiskárny
            SerialPortError,          // Chyba sériového portu
            ConfigurationError,       // Chyba v konfiguraci
            ProgramWarning,           // Varování programu
            AbnormalData,             // Nestandardní data QR
            PlcError,                 // Chyba PLC
            SemaphoreError            // Chyba semaforu
        }


        /// <summary>
        /// Výčtový typ reprezentující různé typy zpráv.
        /// </summary>
        public enum TypeMessage
        {
            Warning,     // Varování
            Information, // Informace
            Critical     // Kritická zpráva
        }

        /// <summary>
        /// Metoda pro žádost o uzavření aplikace.
        /// </summary>
        /// <param name="sender">Odesílatel události</param>
        /// <param name="e">Argumenty události</param>
        private void Exit(object sender, RoutedEventArgs e)
        {
            try
            {
                // Nastavení příznaku pro žádost o uzavření aplikace.
                IsRequestToAppClose = true;

                // Zavření aktuálního okna.
                Close();
            }
            catch (Exception ex)
            {
                // Zobrazení chybové zprávy v případě výjimky.
                MessageBox.Show($"Chyba: {ex.Message}");
            }
        }


        /// <summary>
        /// Metoda pro uzavření aktuálního okna aplikace.
        /// </summary>
        /// <param name="sender">Odesílatel události</param>
        /// <param name="e">Argumenty události</param>
        private void Close(object sender, RoutedEventArgs e)
        {
            try
            {
                // Zavření aktuálního okna.
                Close();
            }
            catch (Exception ex)
            {
                // Zobrazení chybové zprávy v případě výjimky.
                MessageBox.Show($"Chyba: {ex.Message}");
            }
        }


        /// <summary>
        /// Metoda pro odhlášení uživatele s možností potvrzení uživatelem.
        /// </summary>
        /// <param name="sender">Odesílatel události</param>
        /// <param name="e">Argumenty události</param>
        private void Logoff(object sender, RoutedEventArgs e)
        {
            try
            {
                // Zobrazení dialogu pro potvrzení odhlášení uživatele.
                var result = MessageBox.Show("Skutečně chcete odhlásit počítač?", "Odhlášení uživatele", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Provedení odhlášení uživatele.
                    ExitWindowsEx.PCShutdown.LogOff();
                    IsRequestToAppClose = true;
                }
            }
            catch (Exception ex)
            {
                // Zobrazíme chybovou zprávu v případě výjimky.
                MessageBox.Show($"Chyba: {ex.Message}");
            }
        }

        /// <summary>
        /// Metoda pro restart počítače s možností potvrzení uživatelem.
        /// </summary>
        /// <param name="sender">Odesílatel události</param>
        /// <param name="e">Argumenty události</param>
        private void RestartPC(object sender, RoutedEventArgs e)
        {
            try
            {
                // Zobrazení dialogu pro potvrzení restartu počítače.
                var result = MessageBox.Show("Skutečně chcete restartovat počítač?", "Restart počítače", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Spustíme restart počítače.
                    ExitWindowsEx.PCShutdown.Reboot();
                    IsRequestToAppClose = true;
                }
            }
            catch (Exception ex)
            {
                // Zobrazení chybové zprávy v případě výjimky.
                MessageBox.Show($"Chyba: {ex.Message}");
            }
        }


        /// <summary>
        /// Metoda pro vypnutí počítače s možností potvrzení uživatelem.
        /// </summary>
        /// <param name="sender">Odesílatel události</param>
        /// <param name="e">Argumenty události</param>
        private void ShutdownPC(object sender, RoutedEventArgs e)
        {
            try
            {
                // Zobrazení dialogu pro potvrzení vypnutí počítače.
                var result = MessageBox.Show("Skutečně chcete vypnout počítač?", "Vypnutí počítače", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Nastavení příznaku pro požadavek na zavření aplikace.
                    IsRequestToAppClose = true;

                    // Zavolání metody pro pokus o hybridní vypnutí počítače.
                    ExitWindowsEx.PCShutdown.TryHybrid();
                }
            }
            catch (Exception ex)
            {
                // Zobrazení chybové zprávy v případě výjimky.
                MessageBox.Show($"Chyba: {ex.Message}");
            }
        }


        /// <summary>
        /// Metoda pro ukončení jiné instance aplikace po potvrzení od uživatele.
        /// </summary>
        /// <param name="sender">Odesílatel události</param>
        /// <param name="e">Argumenty události</param>
        private void Kill(object sender, RoutedEventArgs e)
        {
            try
            {
                // Zkontrolujte, zda je název aplikace definován.
                if (string.IsNullOrEmpty(ViewModel.AppName))
                {
                    MessageBox.Show("Název aplikace k ukončení nebyl specifikován.");
                    return;
                }

                // Zobrazení dialogu pro potvrzení ukončení jiné instance aplikace.
                var result = MessageBox.Show("Skutečně chcete ukončit jinou instanci aplikace?", "KILL", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Volání metody pro ukončení jiných instancí aplikace.
                    AppConfigure.SystemInfoUtility systemInfoUtility = new AppConfigure.SystemInfoUtility(ViewModel.AppName);
                    systemInfoUtility.UzavřiOstatníInstance();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Třída pro manipulaci se systémovým menu okna, včetně zakázání tlačítka pro uzavření.
    /// </summary>
    public class SystemMenuManipulator
    { 
        /// <summary>
       /// Zakáže tlačítko pro uzavření okna v systémovém menu na základě manipulace s handle (HWND).
       /// </summary>
       /// <param name="hwnd">Handle (HWND) okna, u kterého chcete zakázat tlačítko pro uzavření.</param>
        public static void DisableExitButton(IntPtr hwnd)
        {
            IntPtr hMenu = GetSystemMenu(hwnd, false);
            EnableMenuItem(hMenu, ScClose, MfGrayed);
        }

        /// <summary>
        /// P/Invoke funkce pro získání systémového menu okna.
        /// </summary>
        [DllImport("user32.dll")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);


        /// <summary>
        /// P/Invoke funkce pro povolení nebo zakázání položky v menu systémového okna.
        /// </summary>
        [DllImport("user32.dll")]
        static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);


        /// <summary>
        /// Konstanta reprezentující zakázané (neaktivní) stavové menu.
        /// </summary>
        const uint MfGrayed = 0x00000001;

        /// <summary>
        /// Konstanta reprezentující povolené (aktivní) stavové menu.
        /// </summary>
        const uint MfEnabled = 0x00000000;

        /// <summary>
        /// Konstanta reprezentující operaci zavření okna v systémovém menu.
        /// </summary>
        const uint ScClose = 0xF060;
    }
}
