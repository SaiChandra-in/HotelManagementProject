import { Component } from '@angular/core';
import { PaymentService } from '../payment.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-payment-history',
  templateUrl: './payment-history.component.html',
  styleUrl: './payment-history.component.css'
})
export class PaymentHistoryComponent {
  payments: any[]=[];


  constructor(private ss:PaymentService,private router:Router){}

  formHeader="Add Payment";
  paymentId=null;
  reservationId:any;
  totalAmount:any;
  paymentTime="";
  cardDetails="";
  // paymentStatus="";
  showForm=false;
  
  //search part-->1
  searchYear: number | null = null;
  searchMonth: number | null = null;

  ngOnInit(): void {
    this.getPayments();
    // this.getPaymentHistory();//check
  }

//Get All Payments
  // getPayments(){
  //   this.ss.fetchPayments().subscribe(
  //     (data: any[])=>{
  //       this.payments=data;
  //     },
  //     error=>console.error('Error fetching data', error)
  //   )
  // }


   //search payments -->2
   getPayments(): void {
    if (this.searchYear !== null && this.searchMonth !== null) {
      this.ss.fetchPaymentsByYearAndMonth(this.searchYear, this.searchMonth).subscribe(
        (data: any[]) => {
          console.log('Payments fetched by year and month:', data);
          this.payments = data;
        },
        error => console.error('Error fetching payments by year and month', error)
      );
    } else {
      this.ss.fetchPayments().subscribe(
        (data: any[]) => {
          console.log('All payments fetched:', data);
          this.payments = data;
        },
        error => console.error('Error fetching all payments', error)
      );
    }
  }



//Delete Staff by StaffId
deletePayment(paymentId: number): void {
  this.ss.deletePayment(paymentId).subscribe(
    response => {
      console.log('Delete response:', response);  
      this.getPayments();  // Refresh the list of rooms
    },
    error => {
      console.error('Error deleting payment', error);
      
    }
  );
}

//check
openForm(data:any=null){
  this.showForm=true;
  if(data){
    this.reservationId=data.reservationId;
    this.totalAmount=data.totalAmount;
    this.paymentTime=data.paymentTime;
    this.cardDetails=data.cardDetails;
    // this.paymentStatus=data.paymentStatus;
    this.paymentId=data.paymentId;
    this.formHeader="Edit Payment";
  }
  else{
    this.paymentId=null;//
    this.formHeader="Add Payment";
  }
}

closeForm(){
  this.showForm = false;
  this.clearForm();
}
clearForm(){
  this.reservationId=null;
  this.totalAmount=null;
  this.paymentTime="";
  this.cardDetails="";
  // this.paymentStatus="";
}
savePayment(){
  this.showForm=false;

  let body:any={
    
    reservationId:this.reservationId,
    totalAmount:this.totalAmount,
    paymentTime: this.paymentTime,
    cardDetails:this.cardDetails,
    // paymentStatus:this.paymentStatus,
    

  }
  if(this.paymentId){//
    body['paymentId']=this.paymentId;
    this.ss.putPayment(this.paymentId,body).subscribe(
      (res)=>{
        this.getPayments();
      }        
    )
  }
  else{
    this.ss.postPayment(body).subscribe(
      (res)=>{
        this.getPayments();
      }
    );
  }
}

// searchPayments() -->3
searchPayments(): void {
  console.log('Searching payments for year:', this.searchYear, 'and month:', this.searchMonth);
  this.getPayments();
}

logOut(){
  localStorage.removeItem('token');
  localStorage.removeItem('role');
  localStorage.removeItem('adminName');
  this.router.navigate(['/login']);
}
}
