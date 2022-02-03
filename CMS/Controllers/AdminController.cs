﻿using CMS.Data;
using CMS.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public AdminController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EditPage(string title)
        {
            var page = _dbContext.Pages.FirstOrDefault(x => x.Title == title);

            if (page == null)
            {
                page = new Page();
                page.Title = title;

                _dbContext.Pages.Add(page);
                _dbContext.SaveChanges();
            }
     
            return View(page);
        }

        [HttpPost]
        public IActionResult SavePage(string title, string content)
        {
            var page = _dbContext.Pages.FirstOrDefault(x => x.Title == title);

            if (page == null)
            {
                return View("Error");
            }

            page.Content = content;
            
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
