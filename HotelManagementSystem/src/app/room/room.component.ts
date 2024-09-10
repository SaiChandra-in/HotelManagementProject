import { Component, OnInit } from '@angular/core';
import { RoomService } from '../room.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.css']  
})
export class RoomComponent implements OnInit {
  rooms: any[] = [];  

  constructor(private rs: RoomService,private router:Router) {}

  formHeader = "Add Room";
  roomNumber:any;
  roomType="";
  roomDescription="";
  roomStatus="";
  showForm=false;
  roomId=null;
  
//Get Rooms
  ngOnInit(): void {
    this.getRooms()
  }
  
  getRooms(){
    this.rs.fetchRooms().subscribe(
      (data: any[]) => {
        // console.log(data);
        this.rooms = data;  
      },
      error => console.error('Error fetching data', error)
    )
  }

//Delete Room by RoomId
  deleteRoom(roomId: number): void {
    this.rs.deleteRoom(roomId).subscribe(
      response => {
        console.log('Delete response:', response);  
        this.getRooms();  // Refresh the list of rooms
      },
      error => {
        console.error('Error deleting room', error);
        
      }
    );
  }


  openForm(data:any=null){
    this.showForm=true;
    if(data){
      this.roomNumber=data.roomNumber;
      this.roomType=data.roomType;
      this.roomDescription=data.roomDescription;
      this.roomStatus=data.roomStatus;
      this.roomId=data.roomId;//
      this.formHeader="Edit Room";
    }
    else{
      this.roomId=null;//
      this.formHeader="Add Room";
    }
  }
  
  closeForm(){
    this.showForm = false;
    this.clearForm();
  }
  clearForm(){
      this.roomNumber=null;
      this.roomType="";
      this.roomDescription="";
      this.roomStatus="";
  }
  saveRoom(){
    this.showForm=false;

    let body:any={
      roomNumber:this.roomNumber,
      roomType:this.roomType,
      roomDescription:this.roomDescription,
      roomStatus:this.roomStatus
    }
    if(this.roomId){//
      body['roomId']=this.roomId;
      this.rs.putRoom(this.roomId,body).subscribe(
        (res)=>{
          this.getRooms();
        }        
      )
    }
    else{
      this.rs.postRoom(body).subscribe(
        (res)=>{
          this.getRooms();
        }
      )
    }
  }
  logOut(){
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    localStorage.removeItem('adminName');
    // localStorage.removeItem('AdminName');
    this.router.navigate(['/login']);
  }

}
