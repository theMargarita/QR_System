//using Domain.IRepositories;
//using Domain.Models;
//using Infrastructure.Data;
//using Microsoft.EntityFrameworkCore;

//namespace Infrastructure.Repositories
//{
//    public class PaymentRepository : Repository<Payment>, IPaymentRepository
//    {
//        public PaymentRepository(QrDbContext context) : base(context)
//        {
//        }

//        public async Task<decimal> GetTotalPaidAsync(int tabId)
//        {
//           return await _context.Transactions
//            .Where(p => p.TabId == tabId)
//            .SumAsync(t => t.Total);
//        }

//        public static IQueryable<decimal> Total(this IQueryable<Payment> query, int tabId)
//        {
//            //return query
//            //    .Where(q => q.TabId == tabId)
//            //    .SumAsync(p => p.Amount);
//        }
//    }
//}
