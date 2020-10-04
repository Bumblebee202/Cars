import { Component, OnInit } from '@angular/core';
import { DataService } from './data.service';
import { Car } from './car';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  providers: [DataService]
})
export class AppComponent {
  car: Car = new Car();
  cars: Car[];
  tableMode: boolean = true;

  constructor(private dataService: DataService)
  { }

  ngOnInit()
  {
    this.loadCars();
  }

  loadCars()
  {
    this.dataService.getCars()
      .subscribe((data: Car[]) => this.cars = data);
  }

  save()
  {
    if (this.car.id == null)
    {
      this.dataService.createCar(this.car)
        .subscribe((data: Car) => this.cars.push(data));
    }
    else
    {
      this.dataService.updateCar(this.car)
        .subscribe(data => this.loadCars());
    }
    this.cancel();
  }

  editCar(car: Car)
  {
    this.car = car;
  }

  cancel()
  {
    this.car = new Car();
    this.tableMode = true;
  }

  delete(car: Car)
  {
    this.dataService.deleteCar(car.id)
      .subscribe(data => this.loadCars());
  }

  add()
  {
    this.cancel();
    this.tableMode = false;
  }
}
