using E_books.Data.Base;
using E_books.Models;

namespace E_books.Data.Services
{
    public class SuppliersService : EntityBaseRepository<Supplier>, ISuppliersService
    {
        public SuppliersService(AppDbContext context) : base(context)
        {
        }
    }
}
