using JuanApp.BLL.Interfaces;
using JuanApp.DLL.Data;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.BLL.Services;

public class LayoutService(AppDbContext db) : ILayoutService
{
    public async Task<Dictionary<string, string>> GetSettingsAsync()
    {
        return await db.Settings
            .ToDictionaryAsync(s => s.Key, s => s.Value);
    }
}
