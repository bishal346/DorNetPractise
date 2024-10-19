using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Dtos.Stock;
using backend.Interfaces;
using backend.Mappers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/stock")]   
    [ApiController] 
    public class StockController : ControllerBase
    {
        // private readonly ApplicationDBContext _context; 
        private readonly IStockRepository _stockRepo; 
        
        public StockController(/*ApplicationDBContext context*/ IStockRepository stockRepo)
        {
            // _context = context; 
            _stockRepo = stockRepo; 
        }

        // [HttpGet]
        // public async Task<IActionResult> GetAll() 
        // {
        //     var stocks = await _context.Stock.ToListAsync();
        //     // .Select(s => s.ToStockDto()); 

        //     return Ok(stocks); 
        // }

        // [HttpGet("SP")]
        // public async Task<IActionResult> GetById([FromRoute] int id) 
        // {
        //     var stocks = await _context.Stock.FindAsync(id); 

        //     if(stocks == null) 
        //     {
        //         return NotFound(); 
        //     }
            
        //     return Ok(stocks.ToStockDto()); 
        // }

        // [HttpPost] 
        // public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        // {
        //     var stockModel = stockDto.ToStockFromCreateDTO(); 
        //     await _context.Stock.AddAsync(stockModel); 
        //     await _context.SaveChangesAsync(); 
        //     return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto()); 
        // }

        // [HttpPut] 
        // [Route("{id}")] 
        // public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto) 
        // {
        //     var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id); 
        //     if(stockModel == null) {
        //         return NotFound(); 
        //     }

        //     stockModel.Symbol = updateDto.Symbol;
        //     stockModel.CompanyName = updateDto.CompanyName; 
        //     stockModel.Purchase = updateDto.Purchase; 
        //     stockModel.LastDiv = updateDto.LastDiv;
        //     stockModel.Industry = updateDto.Industry; 
        //     stockModel.MarketCapital = updateDto.MarketCapital; 

        //     await _context.SaveChangesAsync(); 

        //     return Ok(stockModel.ToStockDto());
            
        // }

        // [HttpDelete]
        // [Route("{id}")]
        // public async Task<IActionResult> Delete([FromRoute] int id) 
        // {
        //     var stocksModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id); 

        //     if(stocksModel == null) 
        //     {
        //         return NotFound(); 
        //     }
            
        //     _context.Remove(stocksModel); 
        //     await _context.SaveChangesAsync(); 
        //     return NoContent(); 
        // }

        // [HttpGet("/SP{id}")]
        // public async Task<IActionResult> GetAllStorerdProcedure(int id) 
        // {
        //     var stocks = await _context.Stock.FromSqlRaw($"getStockById {id}").ToListAsync();
        //     // .Select(s => s.ToStockDto()); 

        //     return Ok(stocks); 
        // }

        // [HttpGet("/SP/{id}/{market}")]
        // public async Task<IActionResult> UpdateStorerdProcedure(int id, int market) 
        // {
        //     var stocks = await _context.Database.ExecuteSqlRawAsync($"updateStockMarketCapById {id}, {market}");
        //     // .Select(s => s.ToStockDto()); 

        //     return Ok(stocks); 
        // }

        // [HttpPost("/SP/{id}/{company}")]
        // public async Task<IActionResult> UpdateCompanyNameStorerdProcedure(int id, String company) 
        // {
        //     var stocks = await _context.Database.ExecuteSqlRawAsync($"updateStockCompanyNameById {id}, {company}");
        //     // .Select(s => s.ToStockDto()); 

        //     return Ok(stocks); 
        // }
        // [HttpPost("SP/Insert")] 
        // public async Task<IActionResult> InserNewStock([FromBody] CreateStockRequestDto stockDto)
        // {
        //     var stockModel = stockDto.ToStockFromCreateDTO(); 
        //     var stocks = await _context.Database.ExecuteSqlRawAsync($"insertStockById {stockModel.CompanyName}, {stockModel.Symbol}, {stockModel.Industry}, {stockModel.Purchase}, {stockModel.LastDiv}, {stockModel.MarketCapital}");
        //     // .Select(s => s.ToStockDto()); 

        //     return Ok(stocks); 
        // }




        [HttpGet("StockRepo/get")]
        public async Task<IActionResult> GetAllStockRepo() 
        {
            var stocks = await _stockRepo.GetAllAsync();
            // .Select(s => s.ToStockDto()); 

            return Ok(stocks); 
        }

        [HttpGet("StockRepo/getById")]
        public async Task<IActionResult> GetById([FromRoute] int id) 
        {
            var stocks = await _stockRepo.GetSpecificAsync(id); 
            // Console.WriteLine(stocks); 

            if(stocks == null) 
            {
                return NotFound(); 
            }
            
            return Ok(stocks); 
        }

        [HttpPost("StockRepo/addStock")] 
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel =  stockDto.ToStockFromCreateDTO();  
            await _stockRepo.CreateAsync(stockModel); 
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id}, stockModel.ToStockDto()); 
        }

        [HttpPut("StockRepo/updateStock")] 
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto) 
        {
            var stockModel = await _stockRepo.UpdateAsync(id, updateDto); 
            if(stockModel == null) {
                return NotFound(); 
            }

            // stockModel.Symbol = updateDto.Symbol;
            // stockModel.CompanyName = updateDto.CompanyName; 
            // stockModel.Purchase = updateDto.Purchase; 
            // stockModel.LastDiv = updateDto.LastDiv;
            // stockModel.Industry = updateDto.Industry; 
            // stockModel.MarketCapital = updateDto.MarketCapital; 

            // await _context.SaveChangesAsync(); 

            return Ok(stockModel.ToStockDto());
            
        }

        
    }
}
