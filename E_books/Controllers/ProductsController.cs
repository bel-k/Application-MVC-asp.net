using E_books.Data;
using E_books.Data.Services;
using E_books.Data.Static;
using E_books.Data.ViewModels;
using E_books.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;

namespace E_books.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ProductsController : Controller
    {
        private readonly IProductsService _service;
        private readonly ILogger<ProductsController> _logger;
        private readonly IMemoryCache _cache;
        private readonly string cacheKey = "productsCacheKey";
        public ProductsController(IProductsService service, ILogger<ProductsController> logger, IMemoryCache cache )
        {
            _service = service;
            _logger = logger;
            _cache = cache;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation($"Controller Trying to get all Books at :{DateTime.Now.TimeOfDay} ");
            _logger.Log(LogLevel.Information, "Trying to get all Books");
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            if (_cache.TryGetValue(cacheKey, out IEnumerable<Product> allProducts))
            {
                _logger.Log(LogLevel.Information, "Books found in cache");
            }
            else
            {
                _logger.Log(LogLevel.Information, "Books NOT found in cache, Loading ...");

                allProducts = await _service.GetAllAsync(n => n.Category);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(45))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal);

                _cache.Set(cacheKey, allProducts, cacheEntryOptions);
            }
            stopWatch.Stop();
            _logger.Log(LogLevel.Information, $"Passed Time: {stopWatch.ElapsedMilliseconds} ms");
            return View(allProducts);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allProducts = await _service.GetAllAsync(n => n.Category);

            if (!string.IsNullOrEmpty(searchString))
            {
                //var filteredResult = allMovies.Where(n => n.Name.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();

                var filteredResultNew = allProducts
                        .Where(n => n.Title.Contains(searchString, StringComparison.CurrentCultureIgnoreCase) ||
                           n.Description.Contains(searchString, StringComparison.CurrentCultureIgnoreCase))
                           .ToList();
                return View("Index", filteredResultNew);
            }

            return View("Index", allProducts);
        }

        //GET: Products/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var productDetail = await _service.GetProductByIdAsync(id);
            return View(productDetail);
        }

        //GET: Products/Create
        public async Task<IActionResult> Create()
        {
            var productDropdownsData = await _service.GetNewProductDropdownsValues();

            ViewBag.Categories = new SelectList(productDropdownsData.Categories, "Id", "Title");
            ViewBag.Suppliers = new SelectList(productDropdownsData.Suppliers, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewProductVM product)
        {
            if (!ModelState.IsValid)
            {
                var productDropdownsData = await _service.GetNewProductDropdownsValues();

                ViewBag.Categories = new SelectList(productDropdownsData.Categories, "Id", "Title");
                ViewBag.Suppliers = new SelectList(productDropdownsData.Suppliers, "Id", "Name");

                return View(product);
            }

            await _service.AddNewProductAsync(product);
            return RedirectToAction(nameof(Index));
        }

        //GET: Products/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var productDetails = await _service.GetProductByIdAsync(id);
            if (productDetails == null) return View("NotFound");

            var response = new NewProductVM()
            {
                Id = productDetails.Id,
                Title = productDetails.Title,
                Description = productDetails.Description,
                Price = productDetails.Price,
                createdAt = productDetails.createdAt,
                ImageUrl = productDetails.ImageUrl,
                CategoryId = productDetails.CategoryId,
                SupplierId = productDetails.SupplierId,
            };

            var productDropdownsData = await _service.GetNewProductDropdownsValues();

            ViewBag.Categories = new SelectList(productDropdownsData.Categories, "Id", "Title");
            ViewBag.Suppliers = new SelectList(productDropdownsData.Suppliers, "Id", "Name");


            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewProductVM product)
        {
            if (id != product.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var productDropdownsData = await _service.GetNewProductDropdownsValues();

                ViewBag.Categories = new SelectList(productDropdownsData.Categories, "Id", "Title");
                ViewBag.Suppliers = new SelectList(productDropdownsData.Suppliers, "Id", "Name");

                return View(product);
            }

            await _service.UpdateProductAsync(product);
            return RedirectToAction(nameof(Index));
        }

    }
}
