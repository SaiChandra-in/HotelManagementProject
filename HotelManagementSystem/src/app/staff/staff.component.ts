import { Component, OnInit } from '@angular/core';
import { StaffService } from '../staff.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-staff',
  templateUrl: './staff.component.html',
  styleUrl: './staff.component.css'
})
export class StaffComponent implements OnInit{
  
  staffs: any[]=[];

  constructor(private ss:StaffService,private router:Router){}

  formHeader="Add Staff";
  staffName="";
  age:any;
  address="";
  salary:any;
  designation="";
  staffEmail="";
  staffCode="";
  showForm=false;
  staffId=null;

  ngOnInit(): void {
    this.getStaffs()
  }
//Get All Staffs
  getStaffs(){
    this.ss.fetchStaffs().subscribe(
      (data: any[])=>{
        this.staffs=data;
      },
      error=>console.error('Error fetching data', error)
    )
  }

//Delete Staff by StaffId
deleteStaff(staffId: number): void {
  this.ss.deleteStaff(staffId).subscribe(
    response => {
      console.log('Delete response:', response);  
      this.getStaffs();  // Refresh the list of rooms
    },
    error => {
      console.error('Error deleting staff', error);
      
    }
  );
}

//check
openForm(data:any=null){
  this.showForm=true;
  if(data){
    this.staffName=data.staffName;
    this.age=data.age;
    this.address=data.address;
    this.salary=data.salary;
    this.designation=data.designation;
    this.staffEmail=data.staffEmail;
    this.staffCode=data.staffCode;
    this.staffId=data.staffId;
    this.formHeader="Edit Staff";
  }
  else{
    this.staffId=null;//
    this.formHeader="Add Staff";
  }
}

closeForm(){
  this.showForm = false;
  this.clearForm();
}
clearForm(){
  this.staffName="";
  this.age=null;
  this.address="";
  this.salary=null;
  this.designation="";
  this.staffEmail="";
  this.staffCode="";
}
saveStaff(){
  this.showForm=false;

  let body:any={
    
    staffName:this.staffName,
    age: this.age,
    address:this.address,
    salary:this.salary,
    designation:this.designation,
    staffEmail:this.staffEmail,
    staffCode:this.staffCode

  }
  if(this.staffId){//
    body['staffId']=this.staffId;
    this.ss.putStaff(this.staffId,body).subscribe(
      (res)=>{
        this.getStaffs();
      }        
    )
  }
  else{
    this.ss.postStaff(body).subscribe(
      (res)=>{
        this.getStaffs();
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

