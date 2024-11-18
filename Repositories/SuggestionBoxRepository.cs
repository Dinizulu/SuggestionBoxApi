using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SuggestionBoxApi.Data;
using SuggestionBoxApi.DTOs;
using SuggestionBoxApi.Interfaces;
using SuggestionBoxApi.Models;
using System.Linq.Expressions;

namespace SuggestionBoxApi.Repositories
{
    public class SuggestionBoxRepository<T> : ISuggestionBoxRepository<T> where T : class
    {
        private readonly SuggboxContext _context;
        private DbSet<T> _dbSet;
        public SuggestionBoxRepository(SuggboxContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T record)
        {
            await _dbSet.AddAsync(record);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T record)
        {
            _dbSet.Update(record);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if(entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
    }
}
