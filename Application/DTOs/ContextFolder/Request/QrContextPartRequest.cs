namespace Application.DTOs.ContextFolder.Request
{

    // create new context (ex bord)
    public record QrContextPartRequest
    {
        public string Name { get; init; } = string.Empty;
        public string? QrToken { get; set; }

        public int? OwnerId { get; init; }
        public bool ContextPartIsUnique { get; init; } // if true, one user per context part (ex one user per bord-del) ?? 
    }
}
