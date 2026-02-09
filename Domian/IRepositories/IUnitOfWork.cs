namespace Domain.IRepositories
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IContextRepository Contexts { get; }
        IContextPartRepository ContextParts { get; }
        IProductRepository Products { get; }
        IUserTabRepository UserTabs { get; }
        ITransactionRepository Transactions { get; }
        IPaymentRepository Payments { get; }
        Task<int> SaveChangesAsync();
        void Dispose();
    }
}
