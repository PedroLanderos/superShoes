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
        public async Task<ActionResult<ErrorModel<IEnumerable<StoresModel>>>> GetStores()
        {
            try
            {
                var stores = await _context.Stores.ToListAsync();

                var response = new ErrorModel<IEnumerable<StoresModel>>
                {
                    Success = true,
                    Data = stores,
                    TotalElements = stores.Count
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorModel<IEnumerable<StoresModel>>
                {
                    Success = false,
                    ErrorCode = "500",
                    ErrorMessage = ex.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }

        // GET: api/Stores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ErrorModel<StoresModel>>> GetStoresModel(int id)
        {
            try
            {
                var storesModel = await _context.Stores.FindAsync(id);

                if (storesModel == null)
                {
                    return NotFound(new ErrorModel<StoresModel>
                    {
                        Success = false,
                        ErrorCode = "404",
                        ErrorMessage = "Store not found"
                    });
                }

                return Ok(new ErrorModel<StoresModel>
                {
                    Success = true,
                    Data = storesModel
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel<StoresModel>
                {
                    Success = false,
                    ErrorCode = "500",
                    ErrorMessage = ex.Message
                });
            }
        }

        // PUT: api/Stores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStoresModel(int id, StoresModel storesModel)
        {
            if (id != storesModel.Id)
            {
                return BadRequest(new ErrorModel<StoresModel>
                {
                    Success = false,
                    ErrorCode = "400",
                    ErrorMessage = "Invalid store ID"
                });
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
                    return NotFound(new ErrorModel<StoresModel>
                    {
                        Success = false,
                        ErrorCode = "404",
                        ErrorMessage = "Store not found"
                    });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Stores
        [HttpPost]
        public async Task<ActionResult<ErrorModel<StoresModel>>> PostStoresModel(StoresModel storesModel)
        {
            try
            {
                _context.Stores.Add(storesModel);
                await _context.SaveChangesAsync();

                var response = new ErrorModel<StoresModel>
                {
                    Success = true,
                    Data = storesModel
                };

                return CreatedAtAction("GetStoresModel", new { id = storesModel.Id }, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel<StoresModel>
                {
                    Success = false,
                    ErrorCode = "500",
                    ErrorMessage = ex.Message
                });
            }
        }

        // DELETE: api/Stores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStoresModel(int id)
        {
            try
            {
                var storesModel = await _context.Stores.FindAsync(id);
                if (storesModel == null)
                {
                    return NotFound(new ErrorModel<StoresModel>
                    {
                        Success = false,
                        ErrorCode = "404",
                        ErrorMessage = "Store not found"
                    });
                }

                _context.Stores.Remove(storesModel);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel<StoresModel>
                {
                    Success = false,
                    ErrorCode = "500",
                    ErrorMessage = ex.Message
                });
            }
        }

        private bool StoresModelExists(int id)
        {
            return _context.Stores.Any(e => e.Id == id);
        }
    }
}
