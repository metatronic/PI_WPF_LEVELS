using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SampleProject5.Converters
{
    class BoolToVisibilityConverter : IValueConverter
    {
        #region Properties
        public Visibility TrueValue { get; set; }
        public Visibility FalseValue { get; set; }
        public Visibility NullValue { get; set; }
        #endregion
        #region Constructors
        public BoolToVisibilityConverter()
        {
            TrueValue = Visibility.Visible;
            FalseValue = Visibility.Hidden;
            NullValue = Visibility.Hidden;
        }
        #endregion
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return NullValue;
            }
            bool b;
            bool.TryParse(value.ToString(), out b);
            return b ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}