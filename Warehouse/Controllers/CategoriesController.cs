﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Warehouse.DB.Contexts;
using Warehouse.DB.Entities;
using Warehouse.Infrastructure;

namespace Warehouse.Controllers
{
    [SessionExpireFilter]
    public class CategoriesController : Controller
    {
        private readonly WarehouseDbContext _context;

        public CategoriesController(WarehouseDbContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Category.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            Category category = new Category();
            if (id == null)
            {
                TempData[key: "ErrorMsg"] = "No Categories Found";
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }

                category = await _context.Category
                    .FirstOrDefaultAsync(m => m.CategoryID == id);
                if (category == null)
                {
                    TempData[key: "ErrorMsg"] = "No Categories Found";
                }
            }
            return PartialView("_Details", category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryID,CategoryName")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            Category category = new Category();
            if (id == null)
            {
                TempData[key: "ErrorMsg"] = "No Categories Found";
            }
            else
            {
                category = await _context.Category.FindAsync(id);
                if (category == null)
                {
                    TempData[key: "ErrorMsg"] = "No Categories Found";
                }
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryID,CategoryName")] Category category)
        {
            if (id != category.CategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryID))
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
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            Category category = new Category();
            if (id == null)
            {
                TempData[key: "ErrorMsg"] = "No Categories Found";
            }
            else
            {

                category = await _context.Category
               .FirstOrDefaultAsync(m => m.CategoryID == id);
                if (category == null)
                {
                    TempData[key: "ErrorMsg"] = "No Categories Found";
                }
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Category.FindAsync(id);
            if (category != null)
            {
                _context.Category.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.CategoryID == id);
        }
    }
}
