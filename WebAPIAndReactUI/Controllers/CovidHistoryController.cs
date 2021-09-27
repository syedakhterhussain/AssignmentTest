using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIAndReactUI.Data;
using WebAPIAndReactUI.Model;

namespace WebAPIAndReactUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CovidHistoryController : ControllerBase
    {
        private readonly CovidDataContext _context;

        public CovidHistoryController(CovidDataContext context)
        {
            _context = context;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryHistory>>> GetCountryHistories()
        {
            return await _context.CountryHistories.ToListAsync();
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryHistory>> GetCountryHistory(int id)
        {
            var countryHistory = await _context.CountryHistories.FindAsync(id);

            if (countryHistory == null)
            {
                return NotFound();
            }

            return countryHistory;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountryHistory(int id, CountryHistory countryHistory)
        {
            if (id != countryHistory.Id)
            {
                return BadRequest();
            }

            _context.Entry(countryHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryHistoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

       
        [HttpPost]
        public async Task<ActionResult<CountryHistory>> PostCountryHistory(CountryHistory countryHistory)
        {
            _context.CountryHistories.Add(countryHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountryHistory", new { id = countryHistory.Id }, countryHistory);
        }

     
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountryHistory(int id)
        {
            var countryHistory = await _context.CountryHistories.FindAsync(id);
            if (countryHistory == null)
            {
                return NotFound();
            }

            _context.CountryHistories.Remove(countryHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryHistoryExists(int id)
        {
            return _context.CountryHistories.Any(e => e.Id == id);
        }
    }
}
