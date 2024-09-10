import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InventoryService {

  private url ='http://localhost:5126/api/Inventories'
  constructor(private http:HttpClient) { }

  fetchInventories():Observable<any[]>{
    return this.http.get<any>(this.url).pipe(
      map(response => response.$values || [])
    );
  }

  
 
  deleteInventory(inventoryId: number): Observable<string>{
    return this.http.delete<string>(`${this.url}/${inventoryId}`,{responseType:'text' as 'json'});
  }

  
  postInventory(body:any){
    return this.http.post(this.url,body)
  }

  putInventory(id:number,body:any){
    return this.http.put(`${this.url}/${id}`,body)
  }
  
}
