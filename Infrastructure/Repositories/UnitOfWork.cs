using Domain.IRepositories;
using Domian.IRepoistories;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly QrDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IContextRepository _contextRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserTabRepository _userTabRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IPaymentRepository _paymentRepository;
        public UnitOfWork(QrDbContext context)
        {
            _context = context;
        }

        public IUserRepository Users => _userRepository ??= new UserRepository(_context);

        public IContextRepository Contexts => _contextRepository ??= new ContextRepository(_context);

        public IProductRepository Products => throw new NotImplementedException();

        public IUserTabRepository UserTabs => throw new NotImplementedException();

        public ITransactionRepository Transactions => throw new NotImplementedException();

        public IPaymentRepository Payments => throw new NotImplementedException();

        public Task<int> CompleteAsync()
        {
            throw new NotImplementedException();
        }

        //disposable pattern - dispose context - cleanup?
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
