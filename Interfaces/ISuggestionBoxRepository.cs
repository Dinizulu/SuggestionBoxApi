using SuggestionBoxApi.DTOs;
using System.Linq.Expressions;

namespace SuggestionBoxApi.Interfaces
{
    public interface ISuggestionBoxRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T record);
        Task UpdateAsync(T record);
        Task DeleteAsync(int id);
    }
}
