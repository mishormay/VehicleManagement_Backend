using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VehicleManagement.Entities;

namespace VehicleManagement.Controllers
{
    public class VehicleManufacturersController : BaseController
    {
        private readonly MyContext _context;

        public VehicleManufacturersController(MyContext context)
        {
            _context = context;
        }

        // GET: VehicleManufacturers
        public async Task<IActionResult> Index()
        {
            return View(await _context.VehicleManufacturers.ToListAsync());
        }

        // GET: VehicleManufacturers/Details/5
        public async Task<IActionResult>
            Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleManufacturer = await _context.VehicleManufacturers
            .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleManufacturer == null)
            {
                return NotFound();
            }

            return View(vehicleManufacturer);
        }

        // GET: VehicleManufacturers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VehicleManufacturers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            Create([Bind("Id,Name")] VehicleManufacturer vehicleManufacturer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicleManufacturer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleManufacturer);
        }

        // GET: VehicleManufacturers/Edit/5
        public async Task<IActionResult>
            Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleManufacturer = await _context.VehicleManufacturers.FindAsync(id);
            if (vehicleManufacturer == null)
            {
                return NotFound();
            }
            return View(vehicleManufacturer);
        }

        // POST: VehicleManufacturers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Edit(int id, VehicleManufacturer vehicleManufacturer)
        {
            if (id != vehicleManufacturer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicleManufacturer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleManufacturerExists(vehicleManufacturer.Id))
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
            return View(vehicleManufacturer);
        }

        // GET: VehicleManufacturers/Delete/5
        public async Task<IActionResult>
            Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleManufacturer = await _context.VehicleManufacturers
            .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleManufacturer == null)
            {
                return NotFound();
            }

            return View(vehicleManufacturer);
        }

        // POST: VehicleManufacturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            DeleteConfirmed(int id)
        {
            var vehicleManufacturer = await _context.VehicleManufacturers.FindAsync(id);
            _context.VehicleManufacturers.Remove(vehicleManufacturer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleManufacturerExists(int id)
        {
            return _context.VehicleManufacturers.Any(e => e.Id == id);
        }
    }
}
