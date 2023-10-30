﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dialogs.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void UpdateUI([CallerMemberName]string str = null)
        { 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(str));
        }
    }
}
