using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.Stock;
using backend.Models;

namespace backend.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(); 
        Task<Stock?> GetSpecificAsync(int Id); 
        Task<Stock> CreateAsync(Stock stockModel); 
        Task<Stock?> UpdateAsync(int Id, UpdateStockRequestDto stockDto); 

    }
}