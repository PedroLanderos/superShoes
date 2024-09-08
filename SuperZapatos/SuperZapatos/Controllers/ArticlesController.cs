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
        public async Task<ActionResult<ErrorModel<IEnumerable<ArticlesModel>>>> GetArticles()
        {
            try
            {
                var articles = await _context.Articles.ToListAsync();

                var response = new ErrorModel<IEnumerable<ArticlesModel>>
                {
                    Success = true,
                    Data = articles,
                    TotalElements = articles.Count
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorModel<IEnumerable<ArticlesModel>>
                {
                    Success = false,
                    ErrorCode = "500",
                    ErrorMessage = ex.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ErrorModel<ArticlesModel>>> GetArticlesModel(int id)
        {
            try
            {
                var articlesModel = await _context.Articles.FindAsync(id);

                if (articlesModel == null)
                {
                    return NotFound(new ErrorModel<ArticlesModel>
                    {
                        Success = false,
                        ErrorCode = "404",
                        ErrorMessage = "Article not found"
                    });
                }

                return Ok(new ErrorModel<ArticlesModel>
                {
                    Success = true,
                    Data = articlesModel
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel<ArticlesModel>
                {
                    Success = false,
                    ErrorCode = "500",
                    ErrorMessage = ex.Message
                });
            }
        }

        // PUT: api/Articles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticlesModel(int id, ArticlesModel articlesModel)
        {
            if (id != articlesModel.Id)
            {
                return BadRequest(new ErrorModel<ArticlesModel>
                {
                    Success = false,
                    ErrorCode = "400",
                    ErrorMessage = "Invalid article ID"
                });
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
                    return NotFound(new ErrorModel<ArticlesModel>
                    {
                        Success = false,
                        ErrorCode = "404",
                        ErrorMessage = "Article not found"
                    });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Articles
        [HttpPost]
        public async Task<ActionResult<ErrorModel<ArticlesModel>>> PostArticlesModel(ArticlesModel articlesModel)
        {
            try
            {
                _context.Articles.Add(articlesModel);
                await _context.SaveChangesAsync();

                var response = new ErrorModel<ArticlesModel>
                {
                    Success = true,
                    Data = articlesModel
                };

                return CreatedAtAction("GetArticlesModel", new { id = articlesModel.Id }, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel<ArticlesModel>
                {
                    Success = false,
                    ErrorCode = "500",
                    ErrorMessage = ex.Message
                });
            }
        }

        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticlesModel(int id)
        {
            try
            {
                var articlesModel = await _context.Articles.FindAsync(id);
                if (articlesModel == null)
                {
                    return NotFound(new ErrorModel<ArticlesModel>
                    {
                        Success = false,
                        ErrorCode = "404",
                        ErrorMessage = "Article not found"
                    });
                }

                _context.Articles.Remove(articlesModel);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel<ArticlesModel>
                {
                    Success = false,
                    ErrorCode = "500",
                    ErrorMessage = ex.Message
                });
            }
        }

        private bool ArticlesModelExists(int id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
    }
}
