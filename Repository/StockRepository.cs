using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Dtos.Stock;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context; 
        public StockRepository(ApplicationDBContext context)
        {
            _context = context; 
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stock.AddAsync(stockModel); 
            await _context.SaveChangesAsync(); 
            return stockModel; 
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            // const stock = await _context.Stock.ToListAsync(); 
            return await _context.Stock.ToListAsync(); 
        }

        public async Task<Stock?> GetSpecificAsync(int id)
        {
            // Console.Write(id); 
            var stocks = await _context.Stock.FromSqlRaw($"getStockById {id}").ToListAsync();
            // Console.WriteLine(stocks); 
            return await _context.Stock.FirstOrDefaultAsync(x => x.Id+"" == id+""); 
        }

        public async Task<Stock?> UpdateAsync(int Id, UpdateStockRequestDto stockDto)
        {
            var exsistingStock = await _context.Stock.FirstOrDefaultAsync(x => ((x.Id)+"").CompareTo(Id+"")==0); 
            if(exsistingStock == null) {
                return null; 
            }
            exsistingStock.Symbol = stockDto.Symbol;
            exsistingStock.CompanyName = stockDto.CompanyName; 
            exsistingStock.Purchase = stockDto.Purchase; 
            exsistingStock.LastDiv = stockDto.LastDiv;
            exsistingStock.Industry = stockDto.Industry; 
            exsistingStock.MarketCapital = stockDto.MarketCapital; 

            await _context.SaveChangesAsync(); 

            return exsistingStock; 
        }
    }
}