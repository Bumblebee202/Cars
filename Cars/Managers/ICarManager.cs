using Cars.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cars.Managers
{
    public interface ICarManager
    {
        Task<Car> Create(string name, string description);
        Task<Car> Update(string id, string name, string description);
        Task Delete(string id);
        Task<Car> Get(string id);
        Task<Car> GetLast();
        Task<IEnumerable<Car>> Enumerate();
    }
}
