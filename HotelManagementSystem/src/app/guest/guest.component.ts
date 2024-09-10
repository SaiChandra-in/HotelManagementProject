import { Component, OnInit } from '@angular/core';
import { GuestService } from '../guest.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-guest',
  templateUrl: './guest.component.html',
  styleUrl: './guest.component.css'
})
export class GuestComponent implements OnInit{
  guests: any[] = [];  

  constructor(private gs: GuestService,private router:Router) {}

  formHeader = "Add Room";
  guestName="";
  guestEmail="";
  gender="";
  address="";
  phoneNo="";
  password="";
  confirmPassword="";
  showForm=false;
  guestId=null;

  ngOnInit(): void {
    this.getGuests()
  }

  getGuests(){
    this.gs.fetchGuests().subscribe(
      (data: any[]) => {
        // console.log(data);
        this.guests = data;  
      },
      error => console.error('Error fetching data', error)
    )
  }

  deleteGuest(guestId: number): void {
    this.gs.deleteGuest(guestId).subscribe(
      response => {
        console.log('Delete response:', response);  
        alert(response);
        this.getGuests();  // Refresh the list of rooms
      },
      error => {
        console.error('Error deleting guest', error);
        // alert(error);
        
      }
    );
  }

  openForm(data:any=null){
    this.showForm=true;
    if(data){
      this.guestName=data.guestName;
      this.guestEmail=data.guestEmail;
      this.gender=data.gender;
      this.address=data.address;
      this.phoneNo=data.phoneNo;//
      this.guestId=data.guestId;
      this.password=data.password;
      this.confirmPassword=data.confirmPassword;
      this.formHeader="Edit Guest";
    }
    else{
      this.guestId=null;//
      this.formHeader="Add Guest";
    }
  }
  closeForm(){
    this.showForm = false;
    this.clearForm();
  }
  clearForm(){
      this.guestName="";
      this.guestEmail="";
      this.gender="";
      this.address="";
      this.phoneNo="";
      this.password="";
      this.confirmPassword="";
  }
  saveGuest(){
    this.showForm=false;

    let body:any={
      guestName:this.guestName,
      guestEmail:this.guestEmail,
      gender:this.gender,
      address:this.address,
      phoneNo:this.phoneNo,
      password:this.password,
      confirmPassword:this.confirmPassword
    }
    if(this.guestId){//
      body['guestId']=this.guestId;
      this.gs.putGuest(this.guestId,body).subscribe(
        (res)=>{
          this.getGuests();
        }        
      )
    }
    else{
      this.gs.postGuest(body).subscribe(
        (res)=>{
          this.getGuests();
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
