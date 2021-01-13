using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bibliotek.Data;
using Bibliotek.Models;

namespace Bibliotek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookloansController : ControllerBase
    {
        private readonly BibliotekContext _context;

        public BookloansController(BibliotekContext context)
        {
            _context = context;
        }

        // GET: api/Bookloans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bookloan>>> GetBookloan()
        {
            return await _context.Bookloan.ToListAsync();
        }

        // GET: api/Bookloans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bookloan>> GetBookloan(int id)
        {
            var bookloan = await _context.Bookloan.FindAsync(id);

            if (bookloan == null)
            {
                return NotFound();
            }

            return bookloan;
        }

        // PUT: api/Bookloans/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookloan(int id, Bookloan bookloan)
        {
            if (id != bookloan.LoanId)
            {
                return BadRequest();
            }

            bookloan.ReturnDate = DateTime.Now;
            _context.Entry(bookloan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookloanExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Bookloans
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Bookloan>> PostBookloan(Bookloan bookloan)
        {
            bookloan.LoanDate = DateTime.Now;
            _context.Bookloan.Add(bookloan);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookloan", new { id = bookloan.LoanId }, bookloan);
        }

        // DELETE: api/Bookloans/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Bookloan>> DeleteBookloan(int id)
        {
            var bookloan = await _context.Bookloan.FindAsync(id);
            if (bookloan == null)
            {
                return NotFound();
            }

            _context.Bookloan.Remove(bookloan);
            await _context.SaveChangesAsync();

            return bookloan;
        }

        private bool BookloanExists(int id)
        {
            return _context.Bookloan.Any(e => e.LoanId == id);
        }
    }
}
