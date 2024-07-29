using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Warehouse.DB.Contexts;
using Warehouse.DB.Entities;
using Warehouse.Infrastructure;

namespace Warehouse.Controllers
{
    [SessionExpireFilter]
    public class ProductsController : Controller
    {
        private readonly WarehouseDbContext _context;

        public ProductsController(WarehouseDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var warehouseDbContext = _context.Products.Include(p => p.Category);
            return View(await warehouseDbContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            Product product = new Product();
            if (id == null)
            {
                TempData[key: "ErrorMsg"] = "No Products Found";
            }
            else
            {
                product = await _context.Products
                   .Include(p => p.Category)
                   .FirstOrDefaultAsync(m => m.ProductID == id);

                if (product == null)
                {
                    TempData[key: "ErrorMsg"] = "No Products Found";
                }
            }
            return PartialView("_Details", product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryID", "CategoryName");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,Name,Description,Price,Quantity,CategoryID,CategoryName")] Product product)
        {
            if (!string.IsNullOrEmpty(product.Name)
                && !string.IsNullOrEmpty(product.Description)
                && product.Price > 0
                && product.Quantity > 0
                && product.CategoryID > 0
                )
            {
                //if (ModelState.IsValid)
                //{
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryID", "CategoryName", product.CategoryID);
            var errors = ViewData.ModelState.Where(n => n.Value.Errors.Count > 0).ToList();


            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            Product product = new Product();
            if (id == null)
            {
                TempData[key: "ErrorMsg"] = "No Products Found";
            }
            else
            {
                product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    TempData["ErrorMsg"] = "No Products Found";
                }
                else
                {
                    ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryID", "CategoryName", product.CategoryID);
                }
            }
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductID,Name,Description,Price,Quantity,CategoryID")] Product product)
        {
            if (id != product.ProductID)
            {
                TempData[key: "ErrorMsg"] = "No Products Found";
            }
            else
            {

                if (!string.IsNullOrEmpty(product.Name)
                   && !string.IsNullOrEmpty(product.Description)
                   && product.Price > 0
                   && product.Quantity > 0
                   && product.CategoryID > 0
                   )
                {
                    try
                    {
                        _context.Update(product);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProductExists(product.ProductID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryID", "CategoryName", product.CategoryID);
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }
    }
}
