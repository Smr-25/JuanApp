using JuanApp.BLL.Dtos;
using JuanApp.BLL.Interfaces;
using JuanApp.Core.Models;
using JuanApp.DLL.Data;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.BLL.Services;

public class SubscriberService(AppDbContext context) : ISubscriberService
{
    public async Task<bool> SubscribeAsync(string email)
    {
        // Validate email
        if (string.IsNullOrWhiteSpace(email))
            return false;

        // Check if already subscribed
        var existing = await context.Subscribers
            .FirstOrDefaultAsync(s => s.Email == email);

        if (existing != null)
        {
            if (!existing.IsActive)
            {
                existing.IsActive = true;
                await context.SaveChangesAsync();
            }
            return true;
        }

        var subscriber = new Subscriber
        {
            Email = email,
            IsActive = true,
            SubscribedDate = DateTime.UtcNow
        };

        context.Subscribers.Add(subscriber);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<List<SubscriberDto>> GetAllSubscribersAsync()
    {
        var subscribers = await context.Subscribers
            .OrderByDescending(s => s.SubscribedDate)
            .ToListAsync();

        return subscribers.Select(s => new SubscriberDto
        {
            Id = s.Id,
            Email = s.Email,
            IsActive = s.IsActive,
            SubscribedDate = s.SubscribedDate
        }).ToList();
    }

    public async Task<bool> UnsubscribeAsync(int id)
    {
        var subscriber = await context.Subscribers.FindAsync(id);
        if (subscriber == null) return false;

        subscriber.IsActive = false;
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UnsubscribeAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        var subscriber = await context.Subscribers
            .FirstOrDefaultAsync(s => s.Email == email);
        
        if (subscriber == null) return false;

        subscriber.IsActive = false;
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<List<string>> GetActiveSubscriberEmailsAsync()
    {
        return await context.Subscribers
            .Where(s => s.IsActive)
            .Select(s => s.Email)
            .ToListAsync();
    }
}

