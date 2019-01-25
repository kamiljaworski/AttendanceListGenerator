using System.Collections.Generic;

namespace AttendanceListGenerator.Core.Data
{
    public interface IAttendanceListData
    {
        IList<IDay> Days { get; }
        IList<string> Fullnames { get; }
        int MaxNumberOfFullnames { get; }
        Month Month { get; }
        int Year { get; }
    }
}
