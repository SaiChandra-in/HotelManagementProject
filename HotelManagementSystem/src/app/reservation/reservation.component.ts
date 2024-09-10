import { Component, OnInit } from '@angular/core';
import { ReservationService } from '../reservation.service';
import { RoomService } from '../room.service';
import { GuestService } from '../guest.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-reservation',
  templateUrl: './reservation.component.html',
  styleUrl: './reservation.component.css'
})
export class ReservationComponent implements OnInit{
  reservations: any[]=[];
  rooms: any[]=[];
  guests: any[]=[];
  minDate: string | undefined;
  

  constructor(private rr:ReservationService,private rs:RoomService,private gs:GuestService,private router:Router){}//rs

  formHeader="Add Reservation";
  guestId:any;
  roomId:any;
  checkInDate:any;
  checkOutDate:any;
  numberOfAdults:any;
  numberOfChildren:any;
  reservationStatus="";
  price:any;
  taxes:any;
  serviceCost:any;
  totalAmount:any;
  creditCardDetails="";
  paymentTime="";
  paymentStatus="";
  showForm=false;
  reservationId=null;

  ngOnInit(): void {
    this.getReservations()
    this.getRooms()
    this.getGuests()
    const today = new Date();
    this.minDate = today.toISOString().split('T')[0];
    
  }
//Get All Staffs
  getReservations(){
    this.rr.fetchReservations().subscribe(
      (data: any[])=>{
        this.reservations=data;
      },
      error=>console.error('Error fetching data', error)
    )
  }

  //getRooms
  getRooms() {
    this.rs.fetchRooms().subscribe(
      (data: any[]) => {
        this.rooms = data.filter(room=>room.roomStatus==='Available');
      },
      error => console.error('Error fetching rooms', error)
    );
  }

  //Get Guests
  getGuests() {
    this.gs.fetchGuests().subscribe(
      (data: any[]) => {
        this.guests = data;
      },
      error => console.error('Error fetching rooms', error)
    );
  }

//Delete Staff by StaffId
deleteReservation(reservationId: number): void {
  this.rr.deleteReservation(reservationId).subscribe(
    response => {
      console.log('Delete response:', response);  
      this.getReservations();  // Refresh the list of rooms
    },
    error => {
      console.error('Error deleting reservation', error);
      
    }
  );
}

//check
openForm(data:any=null){
  this.showForm=true;
  if(data){
    this.guestId=data.guestId;
    this.roomId=data.roomId;
    this.checkInDate=data.checkInDate;
    this.checkOutDate=data.checkOutDate;
    this.numberOfAdults=data.numberOfAdults;
    this.numberOfChildren=data.numberOfChildren;
    this.reservationStatus=data.reservationStatus;
    this.price=data.price;
    this.taxes=data.taxes;
    this.serviceCost=data.serviceCost;
    this.totalAmount=data.totalAmount;
    this.reservationId=data.reservationId;
    this.creditCardDetails=data.creditCardDetails;
    this.paymentTime=data.paymentTime;
    this.paymentStatus=data.paymentStatus;
    this.formHeader="Edit reservation";
  }
  else{
    this.reservationId=null;//
    this.formHeader="Add reservation";
  }
}

closeForm(){
  this.showForm = false;
  this.clearForm();
}
clearForm(){
  this.guestId=null;
  this.roomId=null;
  this.checkInDate=null;
  this.checkOutDate=null;
  this.numberOfAdults=null;
  this.numberOfChildren=null;
  this.reservationStatus="";
  this.price=null;
  this.taxes=null;
  this.serviceCost=null;
  this.totalAmount=null;
  this.creditCardDetails="";
  this.paymentTime="";
  this.paymentStatus="";
}
saveReservation(){
  this.showForm=false;

  let body:any={
   
    guestId:this.guestId,
    roomId:this.roomId,
    checkInDate:this.checkInDate,
    checkOutDate:this.checkOutDate,
    numberOfAdults:this.numberOfAdults,
    numberOfChildren:this.numberOfChildren,
    reservationStatus:this.reservationStatus,
    price:this.price,
    taxes:this.taxes,
    serviceCost:this.serviceCost,
    totalAmount:this.totalAmount,
    creditCardDetails:this.creditCardDetails,
    paymentTime:this.paymentTime,
    paymentStatus:this.paymentStatus

  }
  if(this.reservationId){//
    body['reservationId']=this.reservationId;
    this.rr.putReservation(this.reservationId,body).subscribe(
      (res)=>{
        this.getReservations();
      }        
    )
  }
  else{
    this.rr.postReservation(body).subscribe(
      (res)=>{
        this.getReservations();
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
