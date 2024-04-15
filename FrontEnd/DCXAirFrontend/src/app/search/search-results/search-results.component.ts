import { Component, Input, OnInit } from '@angular/core';
import { DataService } from '../services/Data.service';
import { CommonResponse } from '../models/Response';
import { Subscription } from 'rxjs';

@Component({
  selector: 'search-results',
  templateUrl: './search-results.component.html',
  styleUrls: ['./search-results.component.css']
})
export class SearchResultsComponent implements OnInit {
  responseDataSubscription?: Subscription;
  responseData?: CommonResponse<any>;

  constructor(private _dataService: DataService) { }

  ngOnInit() {
    this.responseDataSubscription = this._dataService.responseData$.subscribe(data => {
      this.responseData = data;
      // Aquí puedes realizar las acciones necesarias con la respuesta
      this.searchResult(this.responseData);
    });
  }

  searchResult(responseData: CommonResponse<any>) {
    console.log(responseData);
  }

}
