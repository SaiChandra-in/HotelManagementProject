import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  private apiUrl = `http://localhost:5126/api/Guests/Register`;

  constructor(private http: HttpClient) { }

  registerGuest(guestData: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, guestData);
  }
}
