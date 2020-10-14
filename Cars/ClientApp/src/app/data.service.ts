import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Car } from './car';

@Injectable()
export class DataService {

  private url = "/api/cars";

  constructor(private http: HttpClient) {
  }

  getCars() {
    return this.http.get(this.url);
  }

  getCar(id: string) {
    return this.http.get(this.url + '/' + id);
  }

  createCar(car: Car) {
    return this.http.post(this.url, car);
  }

  updateCar(car: Car) {
    return this.http.put(this.url, car);
  }

  //partialUpdateCar(car: Car) {
  //  return this.http.patch(this.url, car);
  //}

  deleteCar(id: string) {
    return this.http.delete(this.url + '/' + id);
  }
}
