using Cars.Adapters;
using Cars.Controllers;
using Cars.Managers;
using Cars.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cars.Tests
{
    public class Tests
    {
        IEnumerable<Car> GetTestCars()
        {
            IEnumerable<Car> cars = new List<Car>
            {
                new Car { Id = "24214afsa", Name = "Audi", Description ="R8" },
                new Car { Id = "214dsafa1", Name = "BMW", Description = "M3" },
                new Car { Id = "12321dsa1", Name = "BMWwwwwww", Description = "M3" },
                new Car { Id = "dsaf121sf", Name = "Chevrolet", Description = "Camaro" },
                new Car { Id = "safsaf124", Name = "Mitsubishi Lancer" }
            };

            return cars;
        }

        [Fact]
        public async void EnumerateNotEmpty()
        {
            Mock<ICarAdapter> mock = new Mock<ICarAdapter>();
            mock.Setup(adapter => adapter.Enumerate())
                .Returns(Task.Run(() => GetTestCars()));

            CarManager carManager = new CarManager(mock.Object);

            CarsController controller = new CarsController(carManager);

            IEnumerable<Car> cars = await controller.Enumerate();

            Assert.NotEmpty(cars);
        }

        [Fact]
        public async void EnumerateEmpty()
        {
            Mock<ICarAdapter> mock = new Mock<ICarAdapter>();
            mock.Setup(adapter => adapter.Enumerate())
                .Returns(Task.Run(() => GetTestCars()));

            CarManager carManager = new CarManager(mock.Object);

            CarsController controller = new CarsController(carManager);

            IEnumerable<Car> cars = await controller.Enumerate();

            Assert.Empty(cars);
        }

        [Fact]
        public async void GetIsNull()
        {
            IEnumerable<Car> cars = GetTestCars();
            string id = "1";

            Mock<ICarAdapter> mock = new Mock<ICarAdapter>();
            mock.Setup(adapter => adapter.Get(id)).
                Returns(Task.Run(() =>
                        cars.FirstOrDefault(c => c.Id.Equals("2"))));

            CarManager carManager = new CarManager(mock.Object);

            CarsController controller = new CarsController(carManager);

            Car car = await controller.Get(id);

            Assert.Null(car);
        }

        [Fact]
        public async void GetIsNotNull()
        {
            IEnumerable<Car> cars = GetTestCars();
            string id = "24214afsa";

            Mock<ICarAdapter> mock = new Mock<ICarAdapter>();
            mock.Setup(adapter => adapter.Get(id)).
                Returns(Task.Run(() =>
                        cars.FirstOrDefault(c => c.Id.Equals(id))));

            CarManager carManager = new CarManager(mock.Object);

            CarsController controller = new CarsController(carManager);

            Car car = await controller.Get(id);

            Assert.NotNull(car);
        }

        [Fact]
        public async void UpdateName()
        {
            string id = "12321dsa1";
            string name = "BMW";
            IEnumerable<Car> cars = GetTestCars();

            Mock<ICarAdapter> mock = new Mock<ICarAdapter>();

            mock.Setup(adapter => adapter.Get(id)).
                Returns(Task.Run(() =>
                        cars.FirstOrDefault(c => c.Id.Equals(id))));

            mock.Setup(adapter => adapter.Update<string>(id, "name", name))
                .Returns(Task.Run(() =>
                {
                    Car car = cars.FirstOrDefault(c => c.Id.Equals(id));
                    car.Name = name;
                }));

            CarManager carManager = new CarManager(mock.Object);

            Car car = await carManager.UpdateName(id, name);

            Assert.Equal(name, car.Name);
        }

        [Fact]
        public async void Delete()
        {
            string id = "12321dsa1";
            List<Car> cars = GetTestCars().ToList();
            int count = cars.Count;

            Mock<ICarAdapter> mock = new Mock<ICarAdapter>();

            mock.Setup(adapter => adapter.Delete(id))
                .Returns(Task.Run(() =>
                {
                    Car car = cars.FirstOrDefault(c => c.Id.Equals(id));
                    cars.Remove(car);
                }));

            CarManager carManager = new CarManager(mock.Object);

            await carManager.Delete(id);

            Assert.NotEqual(count, cars.Count);
        }

        [Fact]
        public async void Create()
        {
            string name = "Name";
            string description = "Description";

            List<Car> cars = GetTestCars().ToList();
            Mock<ICarAdapter> mock = new Mock<ICarAdapter>();

            mock.Setup(adapter => adapter.Create(name, description))
                .Returns(() => Task.Run(() => new Car
                {
                    Id = "dsa124f",
                    Name = name,
                    Description = description
                }));

            CarManager carManager = new CarManager(mock.Object);

            Car car = await carManager.Save(null, name, description);

            Assert.NotNull(car);
        }
    }
}
