import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Currency } from '../models/Currency';
import { FlightLocation } from '../models/FlightLocations';
import { CURRENCIES } from '../Constants/CurrencyConstans';
import { LOCATIONS } from '../Constants/FlightLocations';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonResponse } from '../models/Response';
import { SearchService } from '../search.service';

@Component({
  selector: 'search-form',
  templateUrl: './search-form.component.html',
  styleUrls: ['./search-form.component.css'],
})
export class SearchFormComponent {

  currencies: Currency[] = [];
  flightLocation : FlightLocation[] =[];

  searchForm!: FormGroup;

  private readonly searchObserver = {
    next: (data: CommonResponse<any>) => this.searchResultNext(data),
    error: (error: CommonResponse<any>) => this.searchResultError(error),
    complete: () => console.log("Consulta finalizada")
    ,
  };

  constructor(private _formBuilder: FormBuilder,private _searchService: SearchService) {
    this.createForm();
    this.loadCurrencies();
    this.loadLocations();
  }

  createForm() {
    this.searchForm = this._formBuilder.group({
      origin: ['', Validators.required],
      destination: ['', Validators.required],
      currency: ['', Validators.required],
      tripType: ['oneWay'] 
    });
  }

  loadCurrencies() {
    const constantData = CURRENCIES;

    // Itera sobre los datos y crea objetos Currency
    this.currencies = constantData.map(item => ({
      name: item.name,
      currency: item.currency
    }));    
  }

  loadLocations(){
    const constantData = LOCATIONS;

    this.flightLocation = constantData.map(item => ({
      locationType: item.locationType,
      location: item.location
    }));    
  }

  onSubmit() {
    // Aquí puedes trabajar con los valores del formulario
    console.log(this.searchForm.value);
    var result = this._searchService.searchResult(this.searchForm.value).subscribe(this.searchObserver);
  }

  searchResultNext(data: CommonResponse<any>){
    
  }

  searchResultError(data: CommonResponse<any>){

  }
}
