﻿using System;

namespace AttendanceListGenerator.Core.Data
{
    public interface ILocalizedNames
    {
        string DocumentAuthor { get; }
        string ApplicationDirectoryName { get; }
        string DocumentsDirectoryName { get; }
        string GetDayOfWeekName(DayOfWeek dayOfWeek);
        string GetDayOfWeekAbbreviation(DayOfWeek dayOfWeek);
        string GetMonthName(Month month);
        string GetDocumentTitle(Month month, int Year);
        string GetHolidayName(Holiday holiday);
    }
}
