namespace Application.DTOs.OwnerFolder
{
    public record OwnerAndContextRequest
    {
        public Guid OwnerId { get; set; }
        public string OwnerName { get; init; } = string.Empty;
        public string ContextName { get; init; } = string.Empty;
        public string QrToken { get; init; } = string.Empty;

        public static OwnerAndContextRequest FromDetails(Guid ownerId, string ownerName, string contextName, string qrToken)
        {
            return new OwnerAndContextRequest
            {
                OwnerId = ownerId,
                OwnerName = ownerName,
                ContextName = contextName,
                QrToken = qrToken
            };
        }
    }
}
