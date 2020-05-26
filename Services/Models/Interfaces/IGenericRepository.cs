using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewCore.Models.Interfaces
{
    public interface IGenericRepository<T>
        where T : class
    {
        Task<T> GetById(int id);

        Task<IEnumerable<T>> GetData(int page, int rows);

        Task<int> Create(T entity);

        Task<int> Modify(T entity);

        Task<bool> Remove(int id);
    }
}