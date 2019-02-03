using System.Collections.Generic;

namespace AttendanceListGenerator.Core.Data
{
    public interface IDaysOffList
    {
        IList<IDayOff> AllDaysOff { get; }

        IList<IDayOff> GetDaysOff(Month month);
    }
}
