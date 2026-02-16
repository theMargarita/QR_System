using Application.DTOs.OwnerFolder;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QR_System.Controllers;

public class OwnerService : IOwnerService
{
    private readonly ILogger<OwnerService> _logger;
    private readonly QrDbContext _context;

    public OwnerService(ILogger<OwnerService> logger, QrDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<OwnerResponse> CreateOwnerAsync(string ownerName)
    {
        var owner = new Owner
        {
            //Id = Guid.NewGuid(),
            Name = ownerName
        };

        _context.Owners.Add(owner);
        await _context.SaveChangesAsync();

        _logger.LogInformation($"Created Owner {owner.Id} ({owner.Name})");

        return OwnerResponse.FromOwner(owner);
    }

    public async Task<IEnumerable<OwnerResponse>> GetAllOwnersAsync()
    {
        return await _context.Owners
            .Select(o => OwnerResponse.FromOwner(o)).ToListAsync();

    }

    public async Task<OwnerResponse?> GetOwnerByIdAsync(Guid ownerId)
    {
        var owner = await _context.Owners.FindAsync(ownerId);

        if (owner == null)
        {
            _logger.LogWarning($"Owner {ownerId} not found");
            return null;
        }

        return OwnerResponse.FromOwner(owner);
    }

    public async Task<bool> RemoveOwnerAsync(Guid ownerId)
    {
        var owner = await _context.Owners.FindAsync(ownerId);

        if (owner == null)
        {
            _logger.LogWarning($"Owner {ownerId} not found for deletion");
            return false;
        }

        _context.Owners.Remove(owner);
        await _context.SaveChangesAsync();

        _logger.LogInformation($"Deleted Owner {ownerId}");
        return true;
    }
}
