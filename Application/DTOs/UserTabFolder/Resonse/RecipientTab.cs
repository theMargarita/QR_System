//using Application.DTOs.ProductFolder;
//using Domain.Models;

//namespace Application.DTOs.UserTabFolder.Resonse
//{
//    //Composition Pattern
//    public record TabResponse
//    {
//        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
//        public DateTimeOffset ClosedAt { get; set; } = DateTimeOffset.UtcNow;
//        public UserResponse User { get; set; }
//        public TableResponse Table { get; set; } //contextpart 
//        public List<TransactionResponse> Transactions { get; set; }
//        public List<PaymentResponse> Payments { get; set; }
//        public static TabResponse FromUserTab(UserTab tab)
//        {
//            return new TabResponse
//            {
//                User = new UserResponse
//                {
//                    Id = tab.User.Id,
//                    FirstName = tab.User.FirstName,
//                    LastName = tab.User.LastName
//                },

//                Table = new TableResponse
//                {
//                    Id = tab.ContextPart.Id,
//                    Name = tab.ContextPart.Name
//                },

//                Transactions = tab.Transactions?.Select(t => new TransactionResponse
//                {
//                    Id = t.Id,
//                    //ProductName = t.Product.Name,
//                    Products = t.Products?.Select(p => new ProductListResponse
//                    {
//                        Name = p.Product.Name,
//                        Price = p.Product.Price,
//                        Quantity = p.Quantity
//                    }).ToList() ?? new List<ProductListResponse>(),
//                    Total = t.Total
//                }).ToList() ?? new List<TransactionResponse>(),
//                Payments = tab.Payments?.Select(p => new PaymentResponse
//                {
//                    Id = p.Id,
//                    Amount = p.Amount,
//                    CreatedAt = p.CreatedAt,
//                    Method = p.Method,
//                    Note = p.Note
//                }).ToList() ?? new List<PaymentResponse>(),

//                CreatedAt = tab.CreatedAt,
//                ClosedAt = tab.ClosedAt,
//            };
//        }
//    }

//    public record UserResponse
//    {
//        public Guid Id { get; set; }
//        public string FirstName { get; set; } = string.Empty;
//        public string LastName { get; set; } = string.Empty;
//    }
//    public record TableResponse
//    {
//        public Guid Id { get; set; }
//        public string Name { get; set; } = string.Empty;
//    }

//    public record TransactionResponse
//    {
//        public Guid Id { get; set; } // not sure if this one should be on this one 
//        //public string ProductName { get; set; } = string.Empty;
//        public List<ProductListResponse>? Products { get; set; } 
//        public decimal Total { get; set; }
//    }

//    public record PaymentResponse
//    {
//        public Guid Id { get; set; } //vill man har en betalnings id åtkomst till kvittot? kanske inte - kan vara kvar så länge 
//        public decimal Amount { get; set; }
//        public DateTimeOffset CreatedAt { get; set; }
//        public PaymentMethod Method { get; set; }
//        public string? Note { get; set; }
//    }

//    public record ProductListResponse
//    {
//        public string Name { get; set; } = string.Empty;
//        public decimal Price { get; set; }
//        public int Quantity { get; set; }
//    }
//}
