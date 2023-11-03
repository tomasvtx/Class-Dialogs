using AppConfigure;
using AppConfigure.Model.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using static Dialogs.ErrorDialog;

namespace Dialogs
{
    public static class Dispatch
    {

        private static ErrorDialog errorDialog;

        /// <summary>
        /// Zobrazí chybové dialogové okno a umožní uživateli interagovat.
        /// </summary>
        /// <param name="dispatcher">Dispatcher pro práci s vlákny.</param>
        /// <param name="Description">Popis chyby.</param>
        /// <param name="Text_Conf">Text chybového hlášení.</param>
        /// <param name="Exception">Popis výjimky nebo chybového stavu.</param>
        /// <param name="application">Instance hlavní aplikace.</param>
        /// <param name="Title">Název chybového okna.</param>
        /// <param name="typeMessage">Typ zprávy (Warning, Information, Critical).</param>
        /// <param name="AppInstance">Volitelný parametr pro instance aplikace.</param>
        /// <returns>Operaci Dispatcher, která reprezentuje zobrazení dialogového okna.</returns>
        public static async Task<DispatcherOperation> ShowErrorDialogAsync(this Dispatcher dispatcher, ErrorDialog.Description Description, string Text_Conf, string Exception, Application application, string Title, ErrorDialog.TypeMessage typeMessage, Type AppInstance = null)
        {
            return await Task.FromResult(dispatcher.InvokeAsync(() =>
            {
                /// Otevře nové chybové okno
                Dialogs.ErrorDialog errorDialog = new Dialogs.ErrorDialog(Title, Description, Text_Conf, Exception, typeMessage, AppInstance?.Assembly?.GetName()?.Name);

                /// Čeká na interakci uživatele
                errorDialog.ShowDialog();

                /// Pokud uživatel klikne na tlačítko pro ukončení - vypne hlavní aplikaci
                if (errorDialog.IsRequestToAppClose)
                {
                    application.Shutdown();
                }

            }, DispatcherPriority.Background));
        }


        /// </summary>
        /// <param name="dispatcher"></param>
        /// <param name="Description"></param>
        /// <param name="Text_Conf"></param>
        /// <param name="Exception"></param>
        /// <param name="application"></param>
        /// <param name="Title"></param>
        /// <returns></returns>
        /// <summary>
        /// Zobrazí nové dialogové okno chyby nebo uzavře stávající a zobrazí nové.
        /// </summary>
        /// <param name="dispatcher">Dispatcher pro práci s vlákny.</param>
        /// <param name="Description">Popis chyby.</param>
        /// <param name="Text_Conf">Text chybového hlášení.</param>
        /// <param name="Exception">Popis výjimky nebo chybového stavu.</param>
        /// <param name="application">Instance hlavní aplikace.</param>
        /// <param name="Title">Název chybového okna.</param>
        /// <returns>Operaci Dispatcher, která reprezentuje zobrazení dialogového okna.</returns>
        public static async Task<DispatcherOperation> ZobrazitChybovouZpravu(this Dispatcher dispatcher, ErrorDialog.Description Description, string Text_Conf, string Exception, Application application, string Title)
        {
            /// Pokud je stávající chybové okno otevřené, uzavře ho.
            if (errorDialog != null)
            {
                await dispatcher.InvokeAsync(() => errorDialog.Close(), DispatcherPriority.Normal);
            }

            return await Task.FromResult(dispatcher.InvokeAsync(() =>
            {
                /// Otevře nové chybové okno.
                errorDialog = new Dialogs.ErrorDialog(Title, Description, Text_Conf, Exception, TypeMessage.Information);

                /// Zobrazí dialog bez čekání.
                errorDialog.Show();

                /// Pokud uživatel klikne na tlačítko pro uzavření, ukončí hlavní aplikaci.
                if (errorDialog.IsRequestToAppClose)
                {
                    application.Shutdown();
                }

            }, DispatcherPriority.Background));
        }


        /// <summary>
        /// Uzavře dialogové okno chyby, pokud je otevřeno.
        /// </summary>
        /// <param name="dispatcher">Dispatcher pro práci s vlákny.</param>
        /// <returns>Operaci Task reprezentující uzavření dialogového okna chyby.</returns>
        public static Task ZavritChybovouZpravu(this Dispatcher dispatcher)
        {
            if (errorDialog != null)
            {
                DispatcherOperation operace = dispatcher.InvokeAsync(() => errorDialog?.Close(), DispatcherPriority.Normal);
            }

            return Task.FromResult(0);
        }


        /// <summary>
        /// Nastaví hlavní okno aplikace a provede konfiguraci podle zadaných parametrů.
        /// </summary>
        /// <param name="dispatcher">Dispatcher pro práci s vlákny.</param>
        /// <param name="mainWindow">Hlavní okno aplikace.</param>
        /// <param name="appConfigure">Konfigurace aplikace.</param>
        /// <returns>Operaci Task reprezentující asynchronní načtení hlavního okna.</returns>
        public static Task NastavHlavniOkno(this Dispatcher dispatcher, Window mainWindow, AppConfiguration appConfigure)
        {
            dispatcher.InvokeAsync(() =>
            {
                /// Zobraz hlavní okno
                mainWindow.Show();

                if (appConfigure?.WindowConf != null)
                {
                    /// Nastav šířku hlavního okna podle hodnoty z konfigurace
                    if (appConfigure.WindowConf.Width > 0)
                    {
                        mainWindow.Width = appConfigure.WindowConf.Width ?? double.NaN;
                    }

                    /// Nastav výšku hlavního okna podle hodnoty z konfigurace
                    if (appConfigure.WindowConf.Height > 0)
                    {
                        mainWindow.Height = appConfigure.WindowConf.Height;
                    }

                    /// Nastav pozici "Top" hlavního okna podle hodnoty z konfigurace
                    if (!double.IsNaN(appConfigure.WindowConf.Top))
                    {
                        mainWindow.Top = appConfigure.WindowConf.Top;
                    }

                    /// Nastav pozici "Left" hlavního okna podle hodnoty z konfigurace
                    if (!double.IsNaN(appConfigure.WindowConf.Left))
                    {
                        mainWindow.Left = appConfigure.WindowConf.Left;
                    }

                    if (appConfigure.WindowConf.SecondMonitor == true)
                    {
                        var secondaryScreen = ScreenUtility.ScreenUtility.AllScreens().FirstOrDefault(s => !s.Primary);

                            mainWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                            mainWindow.Left = secondaryScreen.WorkingArea.Left;
                            mainWindow.Top = secondaryScreen.WorkingArea.Top;
                            mainWindow.Width = secondaryScreen.WorkingArea.Width;
                            mainWindow.Height = secondaryScreen.WorkingArea.Height;
                    }

                    mainWindow.WindowState = appConfigure.WindowConf.Fullscreen == true ? WindowState.Maximized : WindowState.Normal;
                }
            }, DispatcherPriority.Send);

            return Task.FromResult(0);
        }

    }

    /// <summary>
    /// Třída obsahující metody pro zpracování speciálních událostí pomocí Dispatcheru.
    /// </summary>
    public static class SpecialDispatch
    {
        /// <summary>
        /// Konstanta obsahující zprávu pro situaci, kdy je program již spuštěný.
        /// </summary>
        public const string ProgramJeJizSpusten = "Program {0} nelze spustit, protože již běží.";

        /// <summary>
        /// Konstanta obsahující zprávu pro situaci, kdy nelze načíst konfigurační nastavení z XML.
        /// </summary>
        public const string ChybaPriCteniXml = "Nelze načíst konfigurační nastavení.";

        /// <summary>
        /// Zobrazí dialogové okno pro situaci, kdy je program již spuštěný.
        /// </summary>
        /// <param name="dispatcher">Dispatcher pro práci s vlákny.</param>
        /// <param name="AppName">Název aplikace.</param>
        /// <param name="application">Instance hlavní aplikace.</param>
        /// <returns>Operaci Dispatcher reprezentující zobrazení dialogového okna.</returns>
        public static async Task<DispatcherOperation> ZobrazChybuProgramJizBeziAsync(this Dispatcher dispatcher, string AppName, Application application)
        {
            return await dispatcher.ShowErrorDialogAsync(ErrorDialog.Description.ConfigurationError, AppName, string.Format(ProgramJeJizSpusten, AppName), application, "Chyba v konfiguraci", ErrorDialog.TypeMessage.Critical);
        }

        /// <summary>
        /// Zobrazí dialogové okno pro situaci, kdy nelze načíst konfigurační nastavení z XML.
        /// </summary>
        /// <param name="dispatcher">Dispatcher pro práci s vlákny.</param>
        /// <param name="Error">Chybová zpráva.</param>
        /// <param name="application">Instance hlavní aplikace.</param>
        /// <returns>Operaci Dispatcher reprezentující zobrazení dialogového okna.</returns>
        public static async Task<DispatcherOperation> ShowXmlReadErrorAsync(this Dispatcher dispatcher, string Error, Application application)
        {
            return await dispatcher.ShowErrorDialogAsync(ErrorDialog.Description.ConfigurationError, ChybaPriCteniXml, Error, application, "Chyba v konfiguraci", ErrorDialog.TypeMessage.Critical);
        }
    }

}
