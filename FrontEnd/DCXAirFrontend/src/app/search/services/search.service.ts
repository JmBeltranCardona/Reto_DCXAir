import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { environment as env } from 'src/environments/environment';
import { catchError, map } from 'rxjs';
import { ResponseHelper } from '../helpers/response.helper';

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  constructor(private _httpClient: HttpClient) { }

  searchResult(request: any) {
    var newTripType = true;
    if (request.tripType != "oneWay") {
      newTripType = false;
    }
  
    let params = new HttpParams()
      .set('Origin', request.origin)
      .set('Destination', request.destination)
      .set('RouteType', newTripType.toString())
      .set('currency', request.currency);
  
    let requestUrl = `${env.url_api}/Journey`;
  
    return this._httpClient.get(requestUrl, { params, observe: 'response' as 'response' })
      .pipe(
        catchError(error => {
          throw ResponseHelper.generateCommonResponseFromHttpErrorResponse(error);
        }),
        map((response: HttpResponse<any>) => {
          return ResponseHelper.generateCommonResponse(response);
        }));
  }
}
