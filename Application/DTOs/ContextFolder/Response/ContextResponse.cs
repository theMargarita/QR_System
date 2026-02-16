using Domain.Models;

namespace Application.DTOs.ContextFolder.Response
{
    public record ContextResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string? QrToken { get; init; }
        public bool IsActive { get; init; }
        public Guid? OwnerId { get; init; }
        public string? OwnerName { get; init; }
        public bool ContextPartIsUnique { get; init; }
        public Guid ContextPartsid { get; init; }
        public string ? PartName { get; set; }

        public static ContextResponse FromContext(Context context)
        {
            var firstPart = context.Parts?.FirstOrDefault();

            return new ContextResponse
            {
                Id = context.Id,
                Name = context.Name,
                QrToken = context.QrToken,
                IsActive = context.IsActive,
                OwnerId = context.OwnerId,
                OwnerName = context.Owner?.Name,
                ContextPartIsUnique = context.ContextPartIsUnique,
            };
        }
    }
}
