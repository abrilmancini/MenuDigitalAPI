using MenuDigitalApi.Models;

namespace MenuDigitalApi.Repositories.Interfaces
{
    public interface IMenuItemRepository
    {
        Task<IEnumerable<MenuItem>> GetAllAsync();
        Task<MenuItem?> GetByIdAsync(int id);
        Task<MenuItem> AddAsync(MenuItem item);
        Task<MenuItem> UpdateAsync(MenuItem item);
        Task DeleteAsync(int id);
    }
}

