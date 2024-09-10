import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReservationService {

  private url ='http://localhost:5126/api/Reservations'
  constructor(private http:HttpClient) { }

  fetchReservations():Observable<any[]>{
    return this.http.get<any>(this.url).pipe(
      map((response: { $values: any; }) => response.$values || [])
    );
  }

  
 
  deleteReservation(staffId: number): Observable<string>{
    return this.http.delete<string>(`${this.url}/${staffId}`,{responseType:'text' as 'json'});
  }

  
  postReservation(body:any){
    return this.http.post(this.url,body,{responseType:'text' as 'json'})
  }

  putReservation(id:number,body:any){
    return this.http.put(`${this.url}/${id}`,body)
  }

}
