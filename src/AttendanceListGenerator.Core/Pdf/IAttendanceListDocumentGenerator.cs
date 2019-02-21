﻿using MigraDoc.DocumentObjectModel;

namespace AttendanceListGenerator.Core.Pdf
{
    public interface IAttendanceListDocumentGenerator
    {
        bool EnableColors { get; set; }
        Color FullnamesBackgroundColor { get; set; }
        Color DayOffBackgroundColor { get; set; }
        Color SaturdayBackgroundColor { get; set; }

        Document GenerateDocument();
    }
}
