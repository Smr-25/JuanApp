using JuanApp.BLL.Dtos;

namespace JuanApp.BLL.Interfaces;

public interface ISettingService
{
    Task<Dictionary<string, string>> GetAllSettingsAsync();
    Task<string?> GetSettingByKeyAsync(string key);
    Task<bool> UpdateSettingAsync(string key, string value);
    Task<bool> UpdateSettingsAsync(Dictionary<string, string> settings);
}

