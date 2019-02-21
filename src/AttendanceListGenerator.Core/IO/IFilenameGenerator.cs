using AttendanceListGenerator.Core.Data;

namespace AttendanceListGenerator.Core.IO
{
    public interface IFilenameGenerator
    {
        string GeneratePdfDocumentFilename(IAttendanceListData data);
        string GenerateJsonSettingsFilename();
    }
}
