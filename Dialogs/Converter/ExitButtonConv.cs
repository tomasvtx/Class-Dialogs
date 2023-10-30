using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using static Dialogs.ErrorDialog;

namespace Dialogs.Converter
{
    /// <summary>
    /// Třída ExitButtonConv implementuje konvertor pro konverzi hodnoty na hodnotu true, pokud typ zprávy je Critical nebo Warning; jinak vrátí false.
    /// </summary>
    public class ExitButtonConv : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TypeMessage data = (TypeMessage)value;

            switch (data)
            {
                case TypeMessage.Critical:
                case TypeMessage.Warning:
                    return true;

                case TypeMessage.Information:
                    return false;

                default: return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
