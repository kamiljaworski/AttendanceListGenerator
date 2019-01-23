using System;

namespace AttendanceListGenerator.Core.Data
{
    public interface ILocalizedNames
    {
        string DocumentAuthor { get; }
        string DocumentComment { get; }
        string GetDayOfWeekName(DayOfWeek dayOfWeek);
        string GetMonthName(Month month);
        string GetDocumentTitle(Month month, int Year);
    }
}
