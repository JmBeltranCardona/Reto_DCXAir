import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Flight } from './models/flight.model';

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  constructor(private _http: HttpClient) { }

  searchFlights(origin: string, destination: string, tripType: string, departureDate: Date) {
    const url = `https://api.example.com/flights?origin=${origin}&destination=${destination}&tripType=${tripType}&departureDate=${departureDate}`;
    return this._http.get<Flight[]>(url);
  }
}
