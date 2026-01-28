namespace Application.DTOs.Requests
{

    // create new context (ex bord)
    public record CreateContextRequest
    {
        public string Name { get; init; } = string.Empty;
        public int? OwnerId { get; init; }
        public bool ContextPartIsUnique { get; init; }
    }
}
