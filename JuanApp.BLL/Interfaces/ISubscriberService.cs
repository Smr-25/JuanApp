using JuanApp.BLL.Dtos;

namespace JuanApp.BLL.Interfaces;

public interface ISubscriberService
{
    Task<bool> SubscribeAsync(string email);
    Task<List<SubscriberDto>> GetAllSubscribersAsync();
    Task<bool> UnsubscribeAsync(int id);
    Task<bool> UnsubscribeAsync(string email);
    Task<List<string>> GetActiveSubscriberEmailsAsync();
}

