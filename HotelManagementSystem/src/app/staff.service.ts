import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StaffService {

  private url ='http://localhost:5126/api/Staffs'
  constructor(private http:HttpClient) { }

  fetchStaffs():Observable<any[]>{
    return this.http.get<any>(this.url).pipe(
      map(response => response.$values || [])
    );
  }

  
  
 
  deleteStaff(staffId: number): Observable<string>{
    return this.http.delete<string>(`${this.url}/${staffId}`,{responseType:'text' as 'json'});
  }

  
  postStaff(body:any){
    return this.http.post(this.url,body)
  }

  putStaff(id:number,body:any){
    return this.http.put(`${this.url}/${id}`,body)
  }
  
}

