using System.Text.Json;
using System.IO;
using System.Windows.Forms; // Application.StartupPath のために必要

namespace EngineerRestApp.Models
{
    public class SettingModel
    {
        public int InitialDelayMinutes { get; set; } = 30; // 休憩開始までの時間 (分)
        public int ImageIntervalSeconds { get; set; } = 30; // 画像が増加する間隔 (秒)
        public string ImageFilePath { get; set; } = Path.Combine(Application.StartupPath, "Assets", "images", "default.png");

        private static string SettingsFilePath => Path.Combine(Application.StartupPath, "settings.json");

        public void Save()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(this, options);
            File.WriteAllText(SettingsFilePath, jsonString);
        }

        public static SettingModel Load()
        {
            if (File.Exists(SettingsFilePath))
            {
                var jsonString = File.ReadAllText(SettingsFilePath);
                return JsonSerializer.Deserialize<SettingModel>(jsonString) ?? new SettingModel();
            }
            return new SettingModel();
        }
    }
}