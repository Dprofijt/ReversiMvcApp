using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReversiMvcApp.Data;
using ReversiMvcApp.Models;

namespace ReversiMvcApp.Controllers
{
 
    public class SpelsController : Controller
    {
        private readonly ReversiDbContext _context;

        public SpelsController(ReversiDbContext context)
        {
            _context = context;
        }

        // GET: Spels
        public async Task<IActionResult> Index(SpelApi spelApi)
        {
            // get list with Spels from SpelApi
            

              return View(await spelApi.GetSpellen());
        }

        // GET: Spels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Spel == null)
            {
                return NotFound();
            }

            var spel = await _context.Spel
                .FirstOrDefaultAsync(m => m.Token == id);
            if (spel == null)
            {
                return NotFound();
            }

            return View(spel);
        }

        // GET: Spels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Spels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Token,Omschrijving,Speler1Token,Speler2Token")] Spel spel)
        {
            //ClaimsPrincipal currentUser = this.User;
            //var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            //spel.Speler1Token = currentUserID;

            spel.Speler1Token = "teate2";


            SpelApi spelApi = new SpelApi();

            if (ModelState.IsValid)
            {
                await spelApi.CreateSpel(spel);
                
                
                //return RedirectToAction(nameof(Index));
            }
            return View(spel);
        }

        // GET: Spels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Spel == null)
            {
                return NotFound();
            }

            var spel = await _context.Spel.FindAsync(id);
            if (spel == null)
            {
                return NotFound();
            }
            return View(spel);
        }

        // POST: Spels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Token,Omschrijving,Speler1Token,Speler1Naam,Speler2Token,Speler2Naam,AanDeBeurt")] Spel spel)
        {
            if (id != spel.Token)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(spel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpelExists(spel.Token))
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
            return View(spel);
        }

        // GET: Spels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Spel == null)
            {
                return NotFound();
            }

            var spel = await _context.Spel
                .FirstOrDefaultAsync(m => m.Token == id);
            if (spel == null)
            {
                return NotFound();
            }

            return View(spel);
        }

        // POST: Spels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Spel == null)
            {
                return Problem("Entity set 'ReversiDbContext.Spel'  is null.");
            }
            var spel = await _context.Spel.FindAsync(id);
            if (spel != null)
            {
                _context.Spel.Remove(spel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpelExists(int id)
        {
          return _context.Spel.Any(e => e.Token == id);
        }
    }
}
