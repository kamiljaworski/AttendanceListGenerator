using System;
using System.Reflection;
using System.Resources;
using AttendanceListGenerator.Core.Data;

namespace AttendanceListGenerator.UI.Localization
{
    public class LocalizedNames : ILocalizedNames
    {
        ResourceManager _strings;

        public LocalizedNames()
        {
            _strings = new ResourceManager("AttendanceListGenerator.UI.Localization.Strings", Assembly.GetExecutingAssembly());
        }

        private string GetLocalizedString(string name)
        {
            try
            {
                return _strings.GetString(name);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public string DocumentAuthor => GetLocalizedString("Author");
        public string ApplicationDirectoryName => GetLocalizedString("ApplicationName");
        public string DocumentsDirectoryName => GetLocalizedString("DocumentsDirectoryName");
        public string GetDayOfWeekName(DayOfWeek dayOfWeek) => GetLocalizedString(dayOfWeek.ToString());
        public string GetMonthName(Month month) => month == Month.None ? string.Empty : GetLocalizedString(month.ToString());
        public string GetHolidayName(Holiday holiday) => holiday == Holiday.None ? string.Empty : GetLocalizedString(holiday.ToString());

        public string GetDayOfWeekAbbreviation(DayOfWeek dayOfWeek)
        {
            string dayOfWeekAbbreviationName = dayOfWeek.ToString() + "Abbreviation";
            return GetLocalizedString(dayOfWeekAbbreviationName);
        }

        public string GetDocumentTitle(Month month, int year)
        {
            string monthName = GetMonthName(month);
            return monthName + " " + year.ToString();
        }
    }
}
