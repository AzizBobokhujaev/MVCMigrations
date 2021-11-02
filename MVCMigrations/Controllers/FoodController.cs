using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCMigrations.Context;
using MVCMigrations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MVCMigrations.Controllers
{
    public class FoodController:Controller
    {
        private readonly MVCContext _context;

        public FoodController(MVCContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var result = await _context.Foods.ToListAsync();
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Food food, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return View(food);
            }

            food.AddedAt = DateTime.UtcNow;

            _context.Foods.Add(food);

            await _context.SaveChangesAsync(token);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var food = await _context.Foods.FindAsync(id);
            if (food == null)
            {
                return RedirectToAction("Index");
            }
            return View(food);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Food foodName)
        {
            if (!ModelState.IsValid)
            {
                return View(foodName);
            }
            var food = await _context.Foods.FindAsync(foodName.Id);
            if (foodName == null)
            {
                return RedirectToAction("Index");
            }
            food.Name = foodName.Name;
            food.Price = foodName.Price;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
            
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var food = await _context.Foods.FindAsync(id);
            if (food == null)
            {
                return RedirectToAction("Index");
            }
            _context.Foods.Remove(food);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
