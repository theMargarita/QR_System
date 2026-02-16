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
        public Guid ContextPartsid { get; init; }
        public string? PartName { get; set; }

        public bool IsTemporary { get; set; }
        public DateTime? StartsAt { get; set; }
        public DateTime? ExpiresAt { get; set; }

        public static CreatedEventResponse FromContext(Context context)
        {
            var firstPart = context.Parts?.FirstOrDefault();

            if (firstPart != null) 
            {
                Console.WriteLine($"Context part {firstPart} is null");
                return null;
            }

            return new CreatedEventResponse
            {
                Id = context.Id,
                Name = context.Name,
                QrToken = context.QrToken,
                IsActive = context.IsActive,
                OwnerId = context.OwnerId ?? Guid.Empty,
                OwnerName = context.Owner?.Name,
                ContextPartIsUnique = context.ContextPartIsUnique,
                ContextPartsid = firstPart?.Id ?? Guid.Empty,
                PartName = firstPart.Name,
                IsTemporary = context.IsTemporary,
                StartsAt = context.StartsAt,
                ExpiresAt = context.ExpiresAt
            };
        }
    }
}
