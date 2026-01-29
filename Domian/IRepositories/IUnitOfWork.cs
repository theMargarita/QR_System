using Domain.IRepositories;

namespace Domain.IRepoistories
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IContextRepository Contexts { get; }
        IProductRepository Products { get; }
        IUserTabRepository UserTabs { get; }
        ITransactionRepository Transactions { get; }
        IPaymentRepository Payments { get; }
        Task<int> CompleteAsync();
        Task<int> SaveChangesAsync();
    }
}
