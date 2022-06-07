using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProgettoFinale.Models;
using ProgettoFinale.Models.Communication;
using ProgettoFinale.Persistence.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProgettoFinale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViaggiController : ControllerBase
    {
        private readonly ILogger<ViaggiController> _logger;
        private readonly IMapper _mapper;
        private DatabaseCxt _context;
        private IOptions<AppSettings> _setting;
        public ViaggiController(ILogger<ViaggiController> logger,
            DatabaseCxt ctx

            )
        {
            _logger = logger;
            _context = ctx;
            // _setting = setting;
            //_mapper = mapper;
        }

        [HttpGet("Cities")]
        public async Task<IActionResult> Get()
        {
            var cities = await _context.City.ToListAsync();
            return Ok(cities);
        }


        [HttpGet("Country/{id}")]
        public async Task<IActionResult> GetByCountry(int id)
        {
            Country c = null;
            using (_context)
            {
                c = await _context.Country.Where(c => c.Id == id).FirstAsync();
                var data = _context.Country
                    .Include(s => s.Cities)
                    .First(c => c.Id == c.Id);

                return Ok(c);
            }

        }

        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Cities/{Name}")]
        public async Task<IActionResult> GetByCity([FromServices] IOptions<AppSettings> setting, string Name)
        {
            City s;
            using (_context)
            {
                try
                {
                    s = await _context.City.Where(c => c.Name == Name).FirstAsync();
                    return Ok(s);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

        }


        [HttpPost]
        public IActionResult Post([FromBody] SaveCityResource value)
        {
            City result = null;
            try
            {
                try
                {
                    result = _context.City.Add(value.ToCity()).Entity;
                    _context.SaveChanges();
                    return Ok(result);
                }
                catch (Exception)
                {

                    throw;
                }
                // var studente = _mapper.Map<SaveStudenteResource, Studente>(value);

            }
            catch (Exception ex)
            {
                throw;
            }

        }


        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody] SaveCityResource payload)
        {
            //var studentePayload = _mapper.Map<SaveStudenteResource, Studente>(payload);
            var std = await _context.City.FindAsync(id);
            City stdRsrc = payload.ToCity();
            std.Name = stdRsrc.Name;
            std.CountryId = stdRsrc.CountryId;
            var result = _context.City.Update(std).Entity;
            try
            {
                var res = await _context.SaveChangesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        //delete city id
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            City s = await _context.City.FindAsync(id);
            _context.City.Remove(s);
            await _context.SaveChangesAsync();
        }
    }
}
