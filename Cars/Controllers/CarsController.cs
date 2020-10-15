using System.Collections.Generic;
using System.Threading.Tasks;
using Cars.Managers;
using Cars.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cars.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        ICarManager _carManager;

        public CarsController(ICarManager carManager) => _carManager = carManager;

        [HttpGet]
        public async Task<IEnumerable<Car>> Enumerate() => await _carManager.Enumerate();

        [HttpGet("{id}")]
        public async Task<Car> Get(string id) => await _carManager.Get(id);

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _carManager.Delete(id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Car car)
        {
            if (ModelState.IsValid)
            {
                car = await _carManager.Save(null, car.Name, car.Description);
                return Ok(car);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Car car)
        {
            if (ModelState.IsValid)
            {
                car = await _carManager.Save(car.Id, car.Name, car.Description);
                return Ok(car);
            }
            return BadRequest(ModelState);
        }

        [HttpPatch("{id}/name")]
        public async Task<IActionResult> UpdateName(string id, [FromBody] CarUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                Car car = await _carManager.UpdateName(id, model.Name);
                return Ok(car);
            }
            return BadRequest(ModelState);
        }

        [HttpPatch("{id}/description")]
        public async Task<IActionResult> UpdateDescription(string id, [FromBody] CarUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                Car car = await _carManager.UpdateDescription(id, model.Description);
                return Ok(car);
            }
            return BadRequest(ModelState);
        }
    }
}
