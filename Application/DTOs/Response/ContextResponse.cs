namespace Application.DTOs.Response
{
    public record ContextResponse
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string? QrToken { get; init; }
        public bool IsActive { get; init; }
        public int? OwnerId { get; init; }
        public string? OwnerName { get; init; }
        public bool ContextPartIsUnique { get; init; }
        public int ContextPartsid { get; init; }
        public string ? PartName { get; set; }
    }
}
