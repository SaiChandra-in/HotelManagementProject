import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RoomService {

  private url = 'http://localhost:5126/api/Rooms';
  
  constructor(private http: HttpClient) { }

  fetchRooms(): Observable<any[]> {
    return this.http.get<any>(this.url).pipe(
      map(response => response.$values || []) // Adjust according to your response structure
    );
  }

  // Method to get rooms by type
  // getRoomsByType(roomType: string): Observable<any[]> {
  // return this.http.get<any[]>(`${this.url}/${roomType}`);
  // }


  deleteRoom(roomId: number): Observable<string> {
    return this.http.delete<string>(`${this.url}/${roomId}`,{responseType:'text' as 'json'});
  }

  postRoom(body:any){
    return this.http.post(this.url,body)
  }

  putRoom(id:number,body:any){
    return this.http.put(`${this.url}/${id}`,body)
  }
}
