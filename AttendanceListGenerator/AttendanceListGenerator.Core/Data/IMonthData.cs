using System.Collections.Generic;

namespace AttendanceListGenerator.Core.Data
{
    public interface IMonthData
    {
        IList<IDay> Days { get; }
        Month Month { get; }
        int Year { get; }
    }
}
