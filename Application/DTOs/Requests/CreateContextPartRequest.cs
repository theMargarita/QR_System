namespace Application.DTOs.Requests
{
    // create new context part (ex borddel)
    public record CreateContextPartRequest
    {
        public string Name { get; init; } = string.Empty;
        public int ContextId { get; init; } // which context (bord) this part belongs to
                                            // (maybe need to change context name?) 
    }
}
