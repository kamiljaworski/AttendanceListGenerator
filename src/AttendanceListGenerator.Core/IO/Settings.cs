using System.Collections.Generic;

namespace AttendanceListGenerator.Core.IO
{
    public class Settings
    {
        public bool EnableColors { get; set; }
        public bool EnableHolidaysTexts { get; set; }
        public bool EnableSundaysTexts { get; set; }
        public bool EnableTableStretching { get; set; }
        public IList<string> Fullnames { get; set; }
    }
}
