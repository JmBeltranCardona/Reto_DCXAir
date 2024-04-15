import { Component, Input, OnInit } from '@angular/core';
import { Flight } from '../models/Flight';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'search-results',
  templateUrl: './search-results.component.html',
  styleUrls: ['./search-results.component.css']
})
export class SearchResultsComponent implements OnInit {
  searchTerm: string = '';

  constructor(private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.searchTerm = this.activatedRoute.snapshot.paramMap.get('searchTerm') || '';
  }

}
