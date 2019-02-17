using System;

namespace AttendanceListGenerator.Core.Data
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; } 
    }
}
