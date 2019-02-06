using AttendanceListGenerator.Core.Data;
using System;
using System.Text;

namespace AttendanceListGenerator.Core.IO
{
    public class FileNameProvider : IFileNameProvider
    {
        private readonly IAttendanceListData _data;
        private readonly ILocalizedNames _names;
        private readonly IDateTimeProvider _dateTimeProvider;

        public FileNameProvider(IAttendanceListData data, ILocalizedNames names, IDateTimeProvider dateTimeProvider)
        {
            if (data == null)
                throw new ArgumentNullException("IAttendanceListData cannot be null");

            if (names == null)
                throw new ArgumentNullException("ILocalizedNames cannot be null");

            if (dateTimeProvider == null)
                throw new ArgumentNullException("IDateTimeProvider cannot be null");

            _data = data;
            _names = names;
            _dateTimeProvider = dateTimeProvider;
        }

        public string GetPdfFileName()
        {
            string fileExtension = ".pdf";

            // Get month localized name and year converted to string
            string documentsMonth = _names.GetMonthName(_data.Month);
            string documentsYear = _data.Year.ToString();

            // Get current DateTime
            DateTime now = _dateTimeProvider.Now;

            // Get current date time converted to string
            string currentYear = now.Year.ToString();
            string currentMonth = GetStringWithZeroAtTheBegnning(now.Month);
            string currentDay = GetStringWithZeroAtTheBegnning(now.Day);
            string currentHour = GetStringWithZeroAtTheBegnning(now.Hour);
            string currentMinute = GetStringWithZeroAtTheBegnning(now.Minute);
            string currentSecond = GetStringWithZeroAtTheBegnning(now.Second);

            // Build a result string
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(documentsMonth);
            stringBuilder.Append("_");
            stringBuilder.Append(documentsYear);
            stringBuilder.Append("_");
            stringBuilder.Append(currentDay);
            stringBuilder.Append(currentMonth);
            stringBuilder.Append(currentYear);
            stringBuilder.Append(currentHour);
            stringBuilder.Append(currentMinute);
            stringBuilder.Append(currentSecond);
            stringBuilder.Append(fileExtension);

            return stringBuilder.ToString();
        }

        private string GetStringWithZeroAtTheBegnning(int number)
        {
            // Format numbers like: '01', '06', '11' etc.
            if (number < 10)
                return "0" + number.ToString();

            return number.ToString();
        }
    }
}
