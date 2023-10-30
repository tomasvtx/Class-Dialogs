using System;
using System.Globalization;
using System.Windows.Data;

namespace Dialogs.Converter
{
    /// <summary>
    /// Třída KillConv implementuje konvertor pro konverzi hodnoty na hodnotu true, pokud vstupní hodnota není null; jinak vrátí false.
    /// </summary>
    public class KillConv : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? true : (object)false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


}
