using Domain.IRepositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly QrDbContext _context;
        private IUserRepository _userRepository;
        private IContextRepository _contextRepository;
        private IContextPartRepository _contextPartRepository;
        private IProductRepository _productRepository;
        private IUserTabRepository _userTabRepository;
        private ITransactionRepository _transactionRepository;
        private IPaymentRepository _paymentRepository;
        public UnitOfWork(QrDbContext context)
        {
            _context = context;
        }

        public IUserRepository Users => _userRepository ??= new UserRepository(_context);

        //these under does not exist yet
        public IContextRepository Contexts => _contextRepository ??= new ContextRepository(_context);
        public IContextPartRepository ContextParts => _contextPartRepository ??= new ContextPartRepository(_context);

        public IProductRepository Products => throw new NotImplementedException();

        public IUserTabRepository UserTabs => throw new NotImplementedException();

        public ITransactionRepository Transactions => throw new NotImplementedException();

        public IPaymentRepository Payments => throw new NotImplementedException();


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
