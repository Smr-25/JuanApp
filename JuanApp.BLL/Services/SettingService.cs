using JuanApp.BLL.Dtos;
using JuanApp.BLL.Interfaces;
using JuanApp.Core.Models;
using JuanApp.DLL.Data;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.BLL.Services;

public class SettingService(AppDbContext context) : ISettingService
{
    public async Task<Dictionary<string, string>> GetAllSettingsAsync()
    {
        return await context.Settings
            .ToDictionaryAsync(s => s.Key, s => s.Value);
    }

    public async Task<string?> GetSettingByKeyAsync(string key)
    {
        var setting = await context.Settings
            .FirstOrDefaultAsync(s => s.Key == key);
        return setting?.Value;
    }

    public async Task<bool> UpdateSettingAsync(string key, string value)
    {
        try
        {
            var setting = await context.Settings
                .FirstOrDefaultAsync(s => s.Key == key);

            if (setting == null)
            {
                setting = new Setting { Key = key, Value = value };
                context.Settings.Add(setting);
            }
            else
            {
                setting.Value = value;
            }

            await context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateSettingsAsync(Dictionary<string, string> settings)
    {
        try
        {
            foreach (var kvp in settings)
            {
                var setting = await context.Settings
                    .FirstOrDefaultAsync(s => s.Key == kvp.Key);

                if (setting == null)
                {
                    setting = new Setting { Key = kvp.Key, Value = kvp.Value };
                    context.Settings.Add(setting);
                }
                else
                {
                    setting.Value = kvp.Value;
                }
            }

            await context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
}

