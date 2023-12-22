using E_books.Data;
using E_books.Data.Services;
using E_books.Data.Static;
using E_books.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace E_books.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class SuppliersController : Controller
    {
        private readonly ISuppliersService _service;
        private readonly ILogger<SuppliersController> _logger;
        public SuppliersController(ISuppliersService service, ILogger<SuppliersController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allSuppliers = await _service.GetAllAsync();
            return View(allSuppliers);
        }
        //Get: suppliers/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ImageUrl,Name,Description")] Supplier supplier)
        {
            if (!ModelState.IsValid) return View(supplier);
            await _service.AddAsync(supplier);
            return RedirectToAction(nameof(Index));
        }

        //Get: Suppliers/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var supplierDetails = await _service.GetByIdAsync(id);
            if (supplierDetails == null) return View("NotFound");
            return View(supplierDetails);
        }

        //Get: Suppliers/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var supplierDetails = await _service.GetByIdAsync(id);
            if (supplierDetails == null) return View("NotFound");
            return View(supplierDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImageUrl,Name,Description")] Supplier supplier)
        {
            if (!ModelState.IsValid) return View(supplier);
            await _service.UpdateAsync(id, supplier);
            return RedirectToAction(nameof(Index));
        }

        //Get: suppliers/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var supplierDetails = await _service.GetByIdAsync(id);
            if (supplierDetails == null) return View("NotFound");
            return View(supplierDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var supplierDetails = await _service.GetByIdAsync(id);
            if (supplierDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
