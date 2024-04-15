import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { CommonResponse } from '../models/Response';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  private responseDataSubject = new BehaviorSubject<CommonResponse<any>>({});
  responseData$ = this.responseDataSubject.asObservable();

  constructor() { }

  setResponseData(data: CommonResponse<any>) {
    this.responseDataSubject.next(data);
  }
}