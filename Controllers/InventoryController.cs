using Microsoft.AspNetCore.Mvc;
using SkaftoBageriWMS.Data;
using SkaftoBageriWMS.Models;
using System.Linq;

namespace SkaftoBageriWMS.Controllers
{
    public class InventoryController : Controller
    {
        private readonly AppDbContext _context;

        public InventoryController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Inventory/Index - Display all inventory items
        public IActionResult Index()
        {
            var inventoryItems = _context.Inventory.ToList();
            return View(inventoryItems);
        }

        // GET: Inventory/Create - Display form to create a new inventory item
        public IActionResult Create()
        {
            return View();
        }

        // POST: Inventory/Create - Add a new inventory item
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                _context.Inventory.Add(inventory);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(inventory);
        }

        // GET: Inventory/Edit/{id} - Display form to edit an inventory item
        public IActionResult Edit(int id)
        {
            var inventory = _context.Inventory.Find(id);
            if (inventory == null)
            {
                return NotFound();
            }
            return View(inventory);
        }

        // POST: Inventory/Edit/{id} - Update an existing inventory item
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Inventory inventory)
        {
            if (id != inventory.InventoryID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _context.Update(inventory);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(inventory);
        }

        // GET: Inventory/Delete/{id} - Display confirmation to delete an inventory item
        public IActionResult Delete(int id)
        {
            var inventory = _context.Inventory.Find(id);
            if (inventory == null)
            {
                return NotFound();
            }
            return View(inventory);
        }

        // POST: Inventory/Delete/{id} - Delete an inventory item
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var inventory = _context.Inventory.Find(id);
            if (inventory != null)
            {
                _context.Inventory.Remove(inventory);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
