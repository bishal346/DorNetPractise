using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/comment")]   
    [ApiController] 
    public class CommentController : ControllerBase
    {
        private readonly ApplicationDBContext _context; 
        public CommentController(ApplicationDBContext context)
        {
            _context = context; 
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            var comments = await _context.Comments.ToListAsync();
            // .Select(s => s.ToStockDto()); 

            return Ok(comments); 
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById([FromRoute] string id) 
        {
            var Id = Convert.ToInt32(id.Trim()); 
            var comments = await _context.Comments.FromSqlRaw($"getCommentById {Id}").ToListAsync(); 
            // .Select(s => s.ToStockDto()); 
            if(comments == null) 
            {
                return NotFound(); 
            }

            return Ok(comments); 
        }

        [HttpDelete("deletComment")]
        public async Task<IActionResult> Delete([FromRoute] int id) 
        {
            var commentModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id); 

            if(commentModel == null) 
            {
                return NotFound(); 
            }
            
            _context.Remove(commentModel); 
            await _context.SaveChangesAsync(); 
            return NoContent(); 
        }
    }
}