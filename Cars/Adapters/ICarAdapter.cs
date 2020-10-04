using Cars.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cars.Adapters
{
    public interface ICarAdapter
    {
        Task Create(string id, string name, string description);
        Task Update(string id, string name, string description);
        Task Delete(string id);
        Task<Car> Get(string id);
        Task<IEnumerable<Car>> Enumerate();
    }
}
