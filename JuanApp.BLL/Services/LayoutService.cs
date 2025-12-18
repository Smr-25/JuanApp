using JuanApp.BLL.Interfaces;
using JuanApp.DLL.Data;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.BLL.Services;

public class LayoutService(AppDbContext dbContext) : ILayoutService
{
    public async Task<Dictionary<string, string>> GetSettingsAsync()
    {
        return await dbContext.Settings
            .ToDictionaryAsync(s => s.Key, s => s.Value);
    }
}
