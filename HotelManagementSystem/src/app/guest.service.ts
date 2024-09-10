import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GuestService {

  private url ='http://localhost:5126/api/Guests'
  constructor(private http:HttpClient) { }

  fetchGuests():Observable<any[]>{
    return this.http.get<any>(this.url).pipe(
      map(response => response.$values || [])
    );
  }

  
 
  deleteGuest(guestId: number): Observable<string>{
    return this.http.delete<string>(`${this.url}/${guestId}`,{responseType:'text' as 'json'});
  }

  
  postGuest(body:any){
    return this.http.post(this.url,body)
  }

  putGuest(id:number,body:any){
    return this.http.put(`${this.url}/${id}`,body,{responseType:'text' as 'json'})
  }
}


