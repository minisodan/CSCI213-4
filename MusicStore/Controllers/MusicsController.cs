using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using MusicStore.Data;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    public class MusicsController : Controller
    {
        private readonly MusicStoreContext _context;

        public MusicsController(MusicStoreContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string MusicGenre, string MusicPerFormer)
        {
            

            if (_context.Music == null)
            {
                return Problem("Entity set 'MusicStoreContext.Music'  is null.");
            }

            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Music
                                            orderby m.genre
                                            select m.genre;

            var music = from m in _context.Music
                        select m;

            IQueryable<string> genreQuery2 = from x in _context.Music
                                             orderby x.genre
                                             select x.performer;

            if (!string.IsNullOrEmpty(MusicGenre))
            {
                genreQuery2 = from x in _context.Music
                              orderby x.genre
                              where x.genre.Contains(MusicGenre)
                              select x.performer;
                music = music.Where(x => x.genre == MusicGenre && x.performer == MusicPerFormer);
            }

            if (!string.IsNullOrEmpty(MusicPerFormer) && !string.IsNullOrEmpty(MusicGenre))
            {
                music = music.Where(x => x.genre == MusicGenre && x.performer == MusicPerFormer);
            }

            var musicGenreVM = new MusicGerneViewModel
            {
                Gernes = new SelectList(await genreQuery.Distinct().ToListAsync()),
             //   Performer = await music.ToListAsync()
            
                Performer = new SelectList(await genreQuery2.Distinct().ToListAsync()),
                Music = await music.ToListAsync()
                
            };

            return View(musicGenreVM);
        }


        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Music
                .FirstOrDefaultAsync(m => m.id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,title,year,performer,typeOfPurchase,genre,price")] Music music)
        {
            if (ModelState.IsValid)
            {
                _context.Add(music);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(music);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Music.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,title,year,performer,genre,price")] Music music)
        {
            if (id != music.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(music);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(music.id))
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
            return View(music);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Music
                .FirstOrDefaultAsync(m => m.id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Music.FindAsync(id);
            if (movie != null)
            {
                _context.Music.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Music.Any(e => e.id == id);
        }
    }
}
