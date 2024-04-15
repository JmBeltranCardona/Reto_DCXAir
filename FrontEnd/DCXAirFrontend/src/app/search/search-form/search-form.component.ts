import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { SearchService } from '../search.service';
import { Flight } from '../models/flight.model';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-search-form',
  templateUrl: './search-form.component.html',
  styleUrls: ['./search-form.component.css'],
})
export class SearchFormComponent implements OnInit {
  searchForm!: FormGroup;
  flights!: Flight[];

  constructor(
    private _searchService: SearchService
  ) { }

  ngOnInit(): void {
    this.searchForm = new FormGroup({
      origin: new FormControl(''),
      destination: new FormControl(''),
      tripType: new FormControl('oneway'),
      departureDate: new FormControl(new Date())
    });
  }

  onSubmit() {
    const { origin, destination, tripType, departureDate } = this.searchForm.value;
    this._searchService.searchFlights(origin, destination, tripType, departureDate).subscribe(flights => {
      this.flights = flights;
    });
  }

}
