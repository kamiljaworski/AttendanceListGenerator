using AttendanceListGenerator.Core.Data;
using AttendanceListGenerator.UI.Localization;
using System;
using System.Globalization;

namespace AttendanceListGenerator.UI.ValueConverters
{
    public class MonthToStringConverter : BaseValueConverter<MonthToStringConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Month month))
                return value != null ? value.ToString() : string.Empty;

            LocalizedNames names = new LocalizedNames();

            return names.GetMonthName(month);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
