namespace Application.DTOs.OwnerFolder
{
    public record OwnerAndContextRequest
    {
        public Guid OwnerId { get; set; }
        public string OwnerName { get; init; } = string.Empty;
        public Guid ContextId { get; set; }
        public string ContextName { get; init; } = string.Empty;
        public string QrToken { get; init; } = string.Empty;

        public static OwnerAndContextRequest FromDetails(Guid ownerId, string ownerName, string contextName, string qrToken, Guid contextId)
        {
            return new OwnerAndContextRequest
            {
                OwnerId = ownerId,
                OwnerName = ownerName,
                ContextId = contextId,
                ContextName = contextName,
                QrToken = qrToken
            };
        }
    }
}
