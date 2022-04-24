import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { ResponseMessage } from '../interfaces/responseMessage';

@Injectable({ providedIn: 'root' })
export class DoctorService {

  private doctorsUrl = '/api/doctors';

  constructor(
    private http: HttpClient) { }

  /** GET doctor by SSN. Will 404 if SSN not found */
  getDoctor(SSN: string): Observable<ResponseMessage> {
    const url = `${this.doctorsUrl}/${SSN}`;
    return this.http.get<ResponseMessage>(url).pipe(
      catchError(this.handleError<ResponseMessage>())
    );
  }

  getDoctors(): Observable<ResponseMessage> {
    return this.http.get<ResponseMessage>(this.doctorsUrl).pipe(
      catchError(this.handleError<ResponseMessage>())
    );
  }

  private handleError<T>(result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);

      return of(result as T);
    };
  }
}
