using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using VehicleManagement.Models;
using VehicleManagement.Helpers;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using System.Net;
using VehicleManagement.Controllers;

namespace WorkFlowHRM.Controllers
{
    //[Authorize]
    [Route("api/v1")]
    [ApiController]
    public class MobileController : BaseController
    {
        private readonly MyContext _context;
        private readonly IOptions<VehicleManagement.Helpers.AppSettings> _options;
       
        public MobileController(MyContext context, IOptions<VehicleManagement.Helpers.AppSettings> options)
        {
            _context = context;
            _options = options;
           
        }


        #region VehicleManagement

        //[Authorize]
        [HttpGet("Manufacturer")]
        public async Task<IActionResult> GetManufacturer()
        {
            var menuf = _context.VehicleManufacturers.Select(x => new { Id = x.Id, Name = x.Name }).ToList();

            return Ok(menuf) ;
        }

        [HttpGet("Manufacturer/ById")]
        public async Task<IActionResult> GetManufacturer(int id)
        {
            var menuf = _context.VehicleManufacturers.Find(id);

            if (menuf != null)
            {
                return Ok(new {id = menuf.Id, Name = menuf.Name});
            }
            else
            {
                return NotFound();
            }
            
        }

        [HttpPost("Manufacturer/Add")]
        public async Task<IActionResult> AddManufacturer(VehicleManufacturer model)
        {
            _context.VehicleManufacturers.Add(new VehicleManufacturer { Id = model.Id, Name = model.Name });
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("Manufacturer/Edit")]
        public async Task<IActionResult> EditManufacturer(VehicleManufacturer model)
        {
            _context.VehicleManufacturers.Add(new VehicleManufacturer { Id = model.Id, Name = model.Name });
            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpGet("Vehicle")]
        public async Task<IActionResult> GetVehicles()
        {
            var menuf = _context.VehicleModels.Include(x=>x.Manufacturer).Select(x => 
                new { 
                    Id = x.Id, 
                    Name = x.Name,
                    ModelYear = x.ModelYear,
                    Manufacturer = x.Manufacturer.Name,
                }
                ).ToList();

            return Ok(menuf);
        }

        [HttpPost("Vehicle/Add")]
        public async Task<IActionResult> AddVehicle(VehicleModel model)
        {
            _context.VehicleModels.Add(new VehicleModel{ Id = model.Id, Name = model.Name, ModelYear = model.ModelYear, ManufacturerId = 1 });
            await _context.SaveChangesAsync();

            return Ok();
        }

        #endregion

    }
}