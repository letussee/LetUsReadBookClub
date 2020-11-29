import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import { Observable,throwError } from 'rxjs';
import { catchError,tap,map} from 'rxjs/operators';


@Injectable(
{providedIn: 'root'}
)
export class LocationAccessService  {
  ipAddressUrl:string="http://api.ipify.org/?format=json";
  countryNameUrl:string="https://ipapi.co/country_name/";
  CountryName:String;
  ipAddress:string;
  


  getIP(): Observable<String> {
    return this.http.get<String>(this.ipAddressUrl)
      .pipe(
        tap(data => console.log('IP api data: ' + JSON.stringify(data))),
        catchError(this.handleError)
      );
  }

  getCName(): Observable<String> {
    return this.http.get(this.countryNameUrl)
    .pipe(map((response: any) => response));
      }
	constructor(private http:HttpClient) { }
  // intercept request and add token
  private handleError(err: HttpErrorResponse): Observable<never> {
    let errorMessage = '';
    if (err.error instanceof ErrorEvent) {
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
    }
    console.error(errorMessage);
    return throwError(errorMessage);
  }
  

}
