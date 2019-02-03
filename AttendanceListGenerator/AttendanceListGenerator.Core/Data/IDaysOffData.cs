using System.Collections.Generic;

namespace AttendanceListGenerator.Core.Data
{
    public interface IDaysOffData
    {
        IList<IDayOff> DaysOff { get; }
        IList<IDayOff> GetDaysOff(Month month);
    }
}
