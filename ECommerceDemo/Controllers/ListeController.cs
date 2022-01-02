using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECommerceDemo.Models;
using ECommerceDemo.Models.Dtos;

namespace ECommerceDemo.Controllers
{
    public class ListeController : Controller
    {
        private readonly SHOPContext _context;

        public ListeController(SHOPContext context)
        {
            _context = context;
        }

        // GET: Videos
        public async Task<IActionResult> Index(string cName)
        {
            if (string.IsNullOrEmpty(cName))
            {
                return RedirectToAction("Index", "Home");
            }
            var category = _context.Categories.FirstOrDefault(i => i.Name == cName);
            var products = category.ProductCategoryMappings.ToList();
            var productIds = products.Select(i => i.ProductId);
            var brands = products.Select(i => i.Product.Brand);
            var video = _context.Videos.Where(i => i.ProductVideoMappings.Any(j => productIds.Contains(j.ProductId))).ToList();
            List<Videos> videos = new List<Videos>();
            foreach (var item in video)
            {
                videos.Add(new Videos
                {
                    Name = item.Name,
                    CreatedDate = item.CreatedDate,
                    Description = item.Description,
                    FileName = item.FileName,
                    FileUrl = item.FileUrl,
                    FirstImageLink = item.FirstImageLink,
                    SecondImageLink = item.SecondImageLink,
                    Brands = brands.Where(i => i.IsBrand).ToList(),
                    Influencer = brands.Where(i => !i.IsBrand).ToList()
                }); 
            }
            return View(videos);
        }
        // GET: Videos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = await _context.Videos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (video == null)
            {
                return NotFound();
            }

            return View(video);
        }

        // GET: Videos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Videos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FileName,FileUrl")] Video video)
        {
            if (ModelState.IsValid)
            {
                _context.Add(video);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(video);
        }

        // GET: Videos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = await _context.Videos.FindAsync(id);
            if (video == null)
            {
                return NotFound();
            }
            return View(video);
        }

        // POST: Videos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FileName,FileUrl")] Video video)
        {
            if (id != video.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(video);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VideoExists(video.Id))
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
            return View(video);
        }

        // GET: Videos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = await _context.Videos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (video == null)
            {
                return NotFound();
            }

            return View(video);
        }

        // POST: Videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var video = await _context.Videos.FindAsync(id);
            _context.Videos.Remove(video);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VideoExists(int id)
        {
            return _context.Videos.Any(e => e.Id == id);
        }
    }
}
