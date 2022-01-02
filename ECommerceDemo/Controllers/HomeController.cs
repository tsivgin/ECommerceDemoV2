using ECommerceDemo.Models;
using ECommerceDemo.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SHOPContext _context;
        public HomeController(ILogger<HomeController> logger, SHOPContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var video = _context.Videos.OrderByDescending(i=>i.CreatedDate).Take(24);
            var brands = _context.Brands.ToList();
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
                    Brands = brands.Where(i=>i.IsBrand).ToList(),
                    Influencer = brands.Where(i => !i.IsBrand).ToList()
                });
            }
            return View(videos);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
