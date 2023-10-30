using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Dialogs.Converter
{
    /// <summary>
    /// Třída DescriptionConv implementuje konvertor pro konverzi hodnoty z enumerace Description na textový popis s úpravami.
    /// </summary>
    public class DescriptionConv : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = (ErrorDialog.Description)value;

            return str.ToString().Replace("__", "/").Replace("_", " ");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
