namespace Application.DTOs.CPFolder.Response
{
    public record ContextPartResponse
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string ? QrToken { get; init; } 
        public bool IsOccupied { get; init; }

        //nav prop
        public int ContextId { get; init; }
        public int UserTabId { get; set; }
    }
}
