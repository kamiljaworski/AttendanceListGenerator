using System;
using AttendanceListGenerator.Core.Data;

namespace AttendanceListGenerator.UI
{
    // TODO: Make actual LocalizedNames class
    public class TempLocalizedNames : ILocalizedNames
    {
        public string DocumentAuthor => "Kamil Jaworski";

        public string DocumentComment => "Lista pracownicza luty 2019";

        public string GetDayOfWeekName(DayOfWeek dayOfWeek)
        {
            throw new NotImplementedException();
        }

        public string GetDocumentTitle(Month month, int Year) => "Luty 2019";

        public string GetMonthName(Month month)
        {
            throw new NotImplementedException();
        }
    }
}
