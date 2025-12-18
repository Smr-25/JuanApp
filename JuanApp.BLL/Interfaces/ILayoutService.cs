namespace JuanApp.BLL.Interfaces;

public interface ILayoutService
{
    Task<Dictionary<string, string>> GetSettingsAsync();
}