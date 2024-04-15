import { Component, Input, OnInit } from '@angular/core';
import { DataService } from '../services/Data.service';
import { CommonResponse } from '../models/Response';
import { Subscription } from 'rxjs';
import { Journey } from '../models/Journey';
import { Flight } from '../models/Flight';

@Component({
  selector: 'search-results',
  templateUrl: './search-results.component.html',
  styleUrls: ['./search-results.component.css']
})
export class SearchResultsComponent implements OnInit {
  responseDataSubscription?: Subscription;
  responseData?: CommonResponse<any>;
  journeys: Journey[] = [];

  constructor(private _dataService: DataService) { }

  ngOnInit() {
    this.responseDataSubscription = this._dataService.responseData$.subscribe(data => {
      this.responseData = data;
      // Aquí puedes realizar las acciones necesarias con la respuesta
      this.searchResult(this.responseData);
    });
  }

  searchResult(responseData: CommonResponse<any>) {
    if (responseData && responseData.data) {
      this.journeys = this.mapResponseToJourney(responseData.data);
      console.log(this.journeys);
      
    } else {
      this.journeys = [];
    }
  }

  private mapResponseToJourney(data: any[]): Journey[] {
    return data.map(item => ({
      origin: item.origin,
      destination: item.destination,
      price: item.price.toString(),
      routeType: true, // Asumiendo que el valor siempre es true en esta respuesta
      flights: this.mapFlights(item.flights)
    }));
  }

  private mapFlights(flights: any[]): Flight[] {
    return flights.map(flight => ({
      origin: flight.origin,
      destination: flight.destination,
      price: flight.price.toString(),
      transport: [{
        flightCarrier: flight.transport.flightCarrier,
        flightNumber: flight.transport.flightNumber
      }]
    }));
  }
}