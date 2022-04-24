import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { ResponseMessage } from '../interfaces/responseMessage';
import { Patient } from '../interfaces/patient';
import { EMRRecord } from '../interfaces/emrRecord';

@Injectable({ providedIn: 'root' })
export class PatientService {

  private patientsUrl = '/api/patients';  // URL to web api

  constructor(
    private http: HttpClient) { }

  /** GET patient from the server */
  getPatients(): Observable<ResponseMessage> {
    return this.http.get<ResponseMessage>(this.patientsUrl)
      .pipe(
        catchError(this.handleError<ResponseMessage>())
      );
  }

  /** GET patient by SSN. Will 404 if SSN not found */
  getPatient(SSN: string): Observable<ResponseMessage> {
    const url = `${this.patientsUrl}/${SSN}`;
    return this.http.get<ResponseMessage>(url).pipe(
      catchError(this.handleError<ResponseMessage>())
    );
  }

  addPatient(patient: Patient): Observable<ResponseMessage> {
    const headers = { "content-type": "application/json" };
    return this.http.post<ResponseMessage>(this.patientsUrl, JSON.stringify(patient), {headers}).pipe(
      catchError(this.handleError<ResponseMessage>())
    );
  }

  addDiagnosis(SSN: string, newRecord: EMRRecord): Observable<ResponseMessage> {
    const headers = { "content-type": "application/json" };
    const url = `${this.patientsUrl}/${SSN}/EMR`;
    return this.http.post<ResponseMessage>(url, JSON.stringify(newRecord), {headers}).pipe(
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
