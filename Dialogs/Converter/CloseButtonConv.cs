using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using static Dialogs.ErrorDialog;
using static System.Net.Mime.MediaTypeNames;

namespace Dialogs.Converter
{
    /// <summary>
    /// Třída CloseButtonConv implementuje konvertor pro konverzi hodnoty enumerace TypeMessage na logickou hodnotu.
    /// Vrací true pro hodnoty TypeMessage Information a Warning, jinak false.
    /// </summary>
    public class CloseButtonConv : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TypeMessage data = (TypeMessage)value;

            switch (data)
            {
                case TypeMessage.Information:
                case TypeMessage.Warning:
                    return true;

                case TypeMessage.Critical:
                    return false;

                default:
                    return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
