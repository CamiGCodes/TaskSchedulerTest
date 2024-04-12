import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HangfireService {
  private apiUrl = 'https://localhost:44390/api/tasks/getHangFireState'; // URL de la API GET

  constructor(private http: HttpClient) { }

  getHangfireStates(): Observable<any> {
    return this.http.get(this.apiUrl);
  }
}
