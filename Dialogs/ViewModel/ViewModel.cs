using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dialogs.ViewModel
{
    public class ViewModel : ViewModelBase
    {
        private string title;
        private Model dialog;
        private string appname;

        public ViewModel()
        {
            Dialog = new Model { Title = "NO TITLE", Description = ErrorDialog.Description.ProgramWarning, Exception = "ERROR" };
        }

        public Model Dialog
        {
            get
            {
                return dialog;
            }
            set
            {
                dialog = value;
                UpdateUI();
            }
        }

        public string AppName
        {
            get
            {
                return appname;
            }
            set
            {
                appname = value;
                UpdateUI();
            }
        }

    }
}
