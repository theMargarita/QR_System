namespace Application.DTOs.OwnerFolder
{
    public class OwnerRequest
    {
        public string Name { get; set; } = string.Empty;
    }


    public record OwnerRequest2(string Name);
}
