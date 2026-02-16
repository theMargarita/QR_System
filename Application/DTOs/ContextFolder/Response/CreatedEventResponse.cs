using System;
using System.Linq;
using Domain.Models;

namespace Application.DTOs.ContextFolder.Response
{
    public record CreatedEventResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string? QrToken { get; init; }
        public bool IsActive { get; init; }
        public Guid? OwnerId { get; init; }
        public string? OwnerName { get; init; }
        public bool ContextPartIsUnique { get; init; }

        // Make nullable so missing part is represented explicitly
        public Guid? ContextPartsid { get; init; }
        public string? PartName { get; init; }

        public bool IsTemporary { get; init; }
        public DateTime? StartsAt { get; init; }
        public DateTime? ExpiresAt { get; init; }

        public static CreatedEventResponse FromContext(Context context)
        {
            var firstPart = context.Parts?.FirstOrDefault();

            return new CreatedEventResponse
            {
                Id = context.Id,
                Name = context.Name,
                QrToken = context.QrToken,
                IsActive = context.IsActive,
                OwnerId = context.OwnerId,
                OwnerName = context.Owner?.Name,
                ContextPartIsUnique = context.ContextPartIsUnique,
                ContextPartsid = firstPart?.Id,
                PartName = firstPart?.Name,
                IsTemporary = context.IsTemporary,
                StartsAt = context.StartsAt,
                ExpiresAt = context.ExpiresAt
            };
        }
    }
}
