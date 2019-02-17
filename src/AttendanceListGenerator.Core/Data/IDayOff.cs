using System;

namespace AttendanceListGenerator.Core.Data
{
    public interface IDayOff
    {
        Holiday Holiday { get; }
        DateTime Date { get; }
    }
}
