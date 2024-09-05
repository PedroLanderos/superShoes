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
    public class ArticlesController : ControllerBase
    {
        private readonly AppDbModel _context;

        public ArticlesController(AppDbModel context)
        {
            _context = context;
        }

        // GET: api/Articles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticlesModel>>> GetArticles()
        {
            return await _context.Articles.ToListAsync();
        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticlesModel>> GetArticlesModel(int id)
        {
            var articlesModel = await _context.Articles.FindAsync(id);

            if (articlesModel == null)
            {
                return NotFound();
            }

            return articlesModel;
        }

        // PUT: api/Articles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticlesModel(int id, ArticlesModel articlesModel)
        {
            if (id != articlesModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(articlesModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticlesModelExists(id))
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

        // POST: api/Articles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ArticlesModel>> PostArticlesModel(ArticlesModel articlesModel)
        {
            _context.Articles.Add(articlesModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticlesModel", new { id = articlesModel.Id }, articlesModel);
        }

        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticlesModel(int id)
        {
            var articlesModel = await _context.Articles.FindAsync(id);
            if (articlesModel == null)
            {
                return NotFound();
            }

            _context.Articles.Remove(articlesModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArticlesModelExists(int id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
    }
}
