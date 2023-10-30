using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dialogs.ErrorDialog;

namespace Dialogs.ViewModel
{
    public class Model
    {
        public string Title { get; set; }
        public Description Description { get; set; }
        public string Instruction { get; set; }
        public string Content { get; set; }
        public string Exception { get; set; }
        public TypeMessage TypeMessage { get; set; }
    }
}
