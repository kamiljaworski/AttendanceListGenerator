using System;
using AttendanceListGenerator.Core.Data;

namespace AttendanceListGenerator.UI
{
    // TODO: Make actual LocalizedNames class
    public class TempLocalizedNames : ILocalizedNames
    {
        public string DocumentAuthor => "Kamil Jaworski";

        public string ApplicationDirectoryName => "Generator list obecności";

        public string DocumentsDirectoryName => "Dokumenty";

        public string GetDayOfWeekAbbreviation(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return "nie.";
                case DayOfWeek.Monday:
                    return "pon.";
                case DayOfWeek.Tuesday:
                    return "wto.";
                case DayOfWeek.Wednesday:
                    return "śro.";
                case DayOfWeek.Thursday:
                    return "czw.";
                case DayOfWeek.Friday:
                    return "pią.";
                case DayOfWeek.Saturday:
                    return "sob.";
                default:
                    return string.Empty;
            }
        }

        public string GetDayOfWeekName(DayOfWeek dayOfWeek) => "Niedziela";

        public string GetDocumentTitle(Month month, int Year) => "Luty 2019";

        public string GetHolidayName(Holiday holiday)
        {
            if (holiday == Holiday.NewYearsDay)
                return "Nowy Rok";

            return "Trzech Króli";
        }

        public string GetMonthName(Month month)
        {
            return "Styczeń";
        }
    }
}
