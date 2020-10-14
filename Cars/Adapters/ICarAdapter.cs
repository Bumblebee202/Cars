using Cars.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cars.Adapters
{
    public interface ICarAdapter
    {
        Task<Car> Create(string name, string description);
        Task<Car> Update(string id, string name, string description);
        Task Delete(string id);
        Task<Car> Get(string id);
        Task<IEnumerable<Car>> Enumerate();
    }
}
