import { Component } from '@angular/core';
import { PaymentService } from '../payment.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrl: './payment.component.css'
})
export class PaymentComponent {
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
  

  ngOnInit(): void {
    this.getPayments()
  }
//Get All Staffs
  getPayments(){
    this.ss.fetchPayments().subscribe(
      (data: any[])=>{
        this.payments=data;
      },
      error=>console.error('Error fetching data', error)
    )
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
    )
  }
}

logOut(){
  localStorage.removeItem('token');
  localStorage.removeItem('role');
  localStorage.removeItem('adminName');
  this.router.navigate(['/login']);
}
}
