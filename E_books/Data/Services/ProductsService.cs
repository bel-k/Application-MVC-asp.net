using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_books.Data.Base;
using E_books.Models;
using E_books.Data.ViewModels;
namespace E_books.Data.Services
{
    public class ProductsService  : EntityBaseRepository<Product>, IProductsService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ProductsService> _logger;
        public ProductsService(AppDbContext context, ILogger<ProductsService> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddNewProductAsync(NewProductVM data)
        {
            var newProduct = new Product()
            {
                Title = data.Title,
                Description = data.Description,
                Price = data.Price,
                ImageUrl = data.ImageUrl,
                SupplierId = data.SupplierId,
                CategoryId = data.CategoryId,
                createdAt = data.createdAt
            };
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var productDetails = await _context.Products
                .Include(c => c.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(n => n.Id == id);

            return productDetails;
        }

        public async Task<NewProductDropdownsVM> GetNewProductDropdownsValues()
        {
            var response = new NewProductDropdownsVM()
            {
                Categories = await _context.Categories.OrderBy(n => n.Title).ToListAsync(),
                Suppliers = await _context.Suppliers.OrderBy(n => n.Name).ToListAsync()
            };
            return response;
        }

        public async Task UpdateProductAsync(NewProductVM data)
        {
            var dbProduct = await _context.Products.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbProduct != null)
            {
                dbProduct.Title = data.Title;
                dbProduct.Description = data.Description;
                dbProduct.Price = data.Price;
                dbProduct.ImageUrl = data.ImageUrl;
                dbProduct.CategoryId = data.CategoryId;
                dbProduct.createdAt = data.createdAt;
                dbProduct.SupplierId = data.SupplierId;
                await _context.SaveChangesAsync();
            }
        }
    }


}
