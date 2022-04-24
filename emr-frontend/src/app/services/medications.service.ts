import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { ResponseMessage } from '../interfaces/responseMessage';
import { Medication } from '../interfaces/medication';

@Injectable({ providedIn: 'root' })
export class MedicationService {

  private medicationsUrl = '/api/medication';  // URL to web api

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private http: HttpClient) { }

  /** GET medications from the server */
  getMedications(): Observable<ResponseMessage> {
    return this.http.get<ResponseMessage>(this.medicationsUrl)
      .pipe(
        catchError(this.handleError<ResponseMessage>())
      );
  }

  /** GET medication by name. Will 404 if name not found */
  getMedication(name: string): Observable<ResponseMessage> {
    const url = `${this.medicationsUrl}/${name}`;
    return this.http.get<ResponseMessage>(url).pipe(
      catchError(this.handleError<ResponseMessage>())
    );
  }

  addMedication(medication: Medication): Observable<ResponseMessage> {
    const headers = { "content-type": "application/json" };
    return this.http.post<ResponseMessage>(this.medicationsUrl, JSON.stringify(medication), { headers }).pipe(
      catchError(this.handleError<ResponseMessage>())
    );
  }

  searchMedication(sdescription: string): Observable<ResponseMessage> {
    const url = `${this.medicationsUrl}/treats/${sdescription}`;
    return this.http.get<ResponseMessage>(url).pipe(
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
