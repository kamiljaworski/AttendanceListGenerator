using System.Collections.Generic;

namespace AttendanceListGenerator.Core.Data
{
    public interface IDaysOffData
    {
        int Year { get; }
        IList<IDayOff> DaysOff { get; }
        IList<IDayOff> GetDaysOff(Month month);
    }
}
