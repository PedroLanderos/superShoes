using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperZapatos.Models;

namespace SuperZapatos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly AppDbModel _context;

        public StoresController(AppDbModel context)
        {
            _context = context;
        }

        // GET: api/Stores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoresModel>>> GetStores()
        {
            return await _context.Stores.ToListAsync();
        }

        // GET: api/Stores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StoresModel>> GetStoresModel(int id)
        {
            var storesModel = await _context.Stores.FindAsync(id);

            if (storesModel == null)
            {
                return NotFound();
            }

            return storesModel;
        }

        // PUT: api/Stores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStoresModel(int id, StoresModel storesModel)
        {
            if (id != storesModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(storesModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoresModelExists(id))
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

        // POST: api/Stores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StoresModel>> PostStoresModel(StoresModel storesModel)
        {
            _context.Stores.Add(storesModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStoresModel", new { id = storesModel.Id }, storesModel);
        }

        // DELETE: api/Stores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStoresModel(int id)
        {
            var storesModel = await _context.Stores.FindAsync(id);
            if (storesModel == null)
            {
                return NotFound();
            }

            _context.Stores.Remove(storesModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StoresModelExists(int id)
        {
            return _context.Stores.Any(e => e.Id == id);
        }
    }
}
