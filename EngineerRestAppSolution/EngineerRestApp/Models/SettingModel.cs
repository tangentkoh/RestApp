using System.Text.Json;
using System.IO;

namespace EngineerRestApp.Models
{
    public class SettingModel
    {
        // 1. 画像が表示され始めるまでの時間 (分単位)
        public int InitialDelayMinutes { get; set; } = 30;

        // 2. 画像が増加していく間隔 (秒単位)
        public int ImageIntervalSeconds { get; set; } = 10;

        // 3. 増加する特定の画像のパス
        // デフォルトはアプリ実行パス内の Assets/images/default.png を想定
        public string ImageFilePath { get; set; } = Path.Combine(Application.StartupPath, "Assets", "images", "default.png");

        private static string SettingsFilePath => Path.Combine(Application.StartupPath, "settings.json");

        // 設定を JSON ファイルに保存する
        public void Save()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(this, options);
            File.WriteAllText(SettingsFilePath, jsonString);
        }

        // JSON ファイルから設定を読み込む
        public static SettingModel Load()
        {
            if (File.Exists(SettingsFilePath))
            {
                try
                {
                    var jsonString = File.ReadAllText(SettingsFilePath);
                    // 読み込みに失敗した場合は新しいデフォルトモデルを返す
                    return JsonSerializer.Deserialize<SettingModel>(jsonString) ?? new SettingModel();
                }
                catch (Exception)
                {
                    // ファイル破損などの場合はデフォルト値を返す
                    return new SettingModel();
                }
            }
            return new SettingModel(); // ファイルが存在しない場合はデフォルト値を返す
        }
    }
}