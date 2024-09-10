import { Component } from '@angular/core';
import { InventoryService } from '../inventory.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-inventory',
  templateUrl: './inventory.component.html',
  styleUrl: './inventory.component.css'
})
export class InventoryComponent {

  inventories: any[]=[];

  constructor(private vs:InventoryService,private router:Router){}

  formHeader="Add Staff";
  itemName="";
  itemDescription="";
  quantity:any;
  unitPrice:any;
  showForm=false;
  inventoryId=null;

  ngOnInit(): void {
    this.getInventories()
  }
//Get All Staffs
  getInventories(){
    this.vs.fetchInventories().subscribe(
      (data: any[])=>{
        this.inventories=data;
      },
      error=>console.error('Error fetching data', error)
    )
  }

//Delete Staff by StaffId
deleteInventory(inventoryId: number): void {
  this.vs.deleteInventory(inventoryId).subscribe(
    response => {
      console.log('Delete response:', response);  
      this.getInventories();  // Refresh the list of rooms
    },
    error => {
      console.error('Error deleting Inventory', error);
      
    }
  );
}

//c
openForm(data:any=null){
  this.showForm=true;
  if(data){
    this.itemName=data.itemName;
    this.itemDescription=data.itemDescription;
    this.quantity=data.quantity;
    this.unitPrice=data.unitPrice;
    this.inventoryId=data.inventoryId;
    this.formHeader="Edit Inventory";
  }
  else{
    this.inventoryId=null;//
    this.formHeader="Add Inventory";
  }
}

closeForm(){
  this.showForm = false;
  this.clearForm();
}
clearForm(){
  this.itemName="";
  this.itemDescription="";
  this.quantity=null;
  this.unitPrice=null;
}
saveInventory(){
  this.showForm=false;

  let body:any={
    
    itemName:this.itemName,
    itemDescription: this.itemDescription,
    quantity:this.quantity,
    unitPrice:this.unitPrice,
  }
  if(this.inventoryId){//
    body['inventoryId']=this.inventoryId;
    this.vs.putInventory(this.inventoryId,body).subscribe(
      (res)=>{
        this.getInventories();
      }        
    )
  }
  else{
    this.vs.postInventory(body).subscribe(
      (res)=>{
        this.getInventories();
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
