using Newtonsoft.Json;

namespace AttendanceListGenerator.Core.IO
{
    public class SettingsSerializer : ISerializer<Settings>
    {
        public string Serialize(Settings value) => JsonConvert.SerializeObject(value);
        public Settings Deserialize(string value) => JsonConvert.DeserializeObject<Settings>(value);
    }
}
