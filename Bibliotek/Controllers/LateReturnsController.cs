using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bibliotek.Data;
using Bibliotek.Models;

namespace Bibliotek.Controllers
{
    public class LateReturnsController : Controller
    {
        private readonly BibliotekContext _context;

        public LateReturnsController(BibliotekContext context)
        {
            _context = context;
        }

        // GET: LateReturns
        public async Task<IActionResult> Index()
        {
            var bibliotekContext = _context.Bookloans.Include(b => b.Customer).Include(b => b.Inventory).ThenInclude(b => b.Book)
                .Where(r => r.DueDate < DateTime.Now && r.ReturnDate == null);
            return View(await bibliotekContext.ToListAsync());
        }

        // GET: LateReturns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookloan = await _context.Bookloans
                .Include(b => b.Customer)
                .Include(b => b.Inventory)
                .FirstOrDefaultAsync(m => m.LoanId == id);
            if (bookloan == null)
            {
                return NotFound();
            }

            return View(bookloan);
        }

        // GET: LateReturns/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FirstName");
            ViewData["InventoryId"] = new SelectList(_context.Inventory, "InventoryId", "InventoryId");
            return View();
        }

        // POST: LateReturns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoanId,InventoryId,CustomerId,LoanDate,ReturnDate")] Bookloan bookloan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookloan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FirstName", bookloan.CustomerId);
            ViewData["InventoryId"] = new SelectList(_context.Inventory, "InventoryId", "InventoryId", bookloan.InventoryId);
            return View(bookloan);
        }

        // GET: LateReturns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookloan = await _context.Bookloans.FindAsync(id);
            if (bookloan == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FirstName", bookloan.CustomerId);
            ViewData["InventoryId"] = new SelectList(_context.Inventory, "InventoryId", "InventoryId", bookloan.InventoryId);
            return View(bookloan);
        }

        // POST: LateReturns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LoanId,InventoryId,CustomerId,LoanDate,ReturnDate")] Bookloan bookloan)
        {
            if (id != bookloan.LoanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookloan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookloanExists(bookloan.LoanId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FirstName", bookloan.CustomerId);
            ViewData["InventoryId"] = new SelectList(_context.Inventory, "InventoryId", "InventoryId", bookloan.InventoryId);
            return View(bookloan);
        }

        // GET: LateReturns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookloan = await _context.Bookloans
                .Include(b => b.Customer)
                .Include(b => b.Inventory)
                .FirstOrDefaultAsync(m => m.LoanId == id);
            if (bookloan == null)
            {
                return NotFound();
            }

            return View(bookloan);
        }

        // POST: LateReturns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookloan = await _context.Bookloans.FindAsync(id);
            _context.Bookloans.Remove(bookloan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookloanExists(int id)
        {
            return _context.Bookloans.Any(e => e.LoanId == id);
        }
    }
}
