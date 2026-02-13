using Domain.Models;

namespace Application.DTOs.OwnerFolder
{
    public record OwnerResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public static OwnerResponse FromOwner(Owner owner)
        {
            return new OwnerResponse
            {
                Id = owner.Id,
                Name = owner.Name
            };
        }
    }
}
