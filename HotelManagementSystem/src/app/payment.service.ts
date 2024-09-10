import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {

  private url ='http://localhost:5126/api/Payments'
  constructor(private http:HttpClient) { }

  fetchPayments():Observable<any[]>{
    return this.http.get<any>(this.url).pipe(
      map(response => response.$values || [])
    );
  }

//payments search by month and year-->1
fetchPaymentsByYearAndMonth(year: number, month: number): Observable<any[]> {
  return this.http.get<any>(`http://localhost:5126/api/Payments/history/payments/${year}/${month}`).pipe(
    map(response => 
      {
        console.log('API Response:', response);
        return response.payments?.$values || []; 
      })
  );
}

  deletePayment(paymentId: number): Observable<string>{
    return this.http.delete<string>(`${this.url}/${paymentId}`,{responseType:'text' as 'json'});
  }

  
  postPayment(body:any){
    return this.http.post(this.url,body)
  }

  putPayment(id:number,body:any){
    return this.http.put(`${this.url}/${id}`,body)
  }
  
}
