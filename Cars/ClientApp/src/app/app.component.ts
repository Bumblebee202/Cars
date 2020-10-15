import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { DataService } from './data.service';
import { Car } from './car';
import { EditCar } from './EditCar';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  providers: [DataService]
})
export class AppComponent {
  @ViewChild('edit', null) editTemplate: TemplateRef<any>;
  @ViewChild('editName', null) editNameTemplate: TemplateRef<any>;
  @ViewChild('editDescription', null) editDescriptionTemplate: TemplateRef<any>;
  car: Car = new Car();
  edit: EditCar = EditCar.Edit;
  cars: Car[];
  tableMode: boolean = true;

  constructor(private dataService: DataService)
  { }

  ngOnInit()
  {
    this.loadCars();
  }

  get getTemplate()
  {
    if (this.edit == EditCar.Edit)
    {
      return this.editTemplate;
    }
    else if (this.edit == EditCar.EditDescription)
    {
      return this.editDescriptionTemplate;
    }
    else if (this.edit == EditCar.EditName)
    {
      return this.editNameTemplate;
    }

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

  updateName()
  {
    this.dataService.updateName(this.car.id, this.car.name)
      .subscribe(data => this.loadCars());
    this.cancel();
  }

  updateDescription()
  {
    this.dataService.updateDescription(this.car.id, this.car.description)
      .subscribe(data => this.loadCars());
    this.cancel();
  }

  editCar(car: Car, edit: EditCar)
  {
    this.car = car;
    this.edit = edit;
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
