import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { RegisterComponent } from './register/register.component';
import { StaffComponent } from './staff/staff.component';
import { RoomComponent } from './room/room.component';
import { InventoryComponent } from './inventory/inventory.component';
import { PaymentComponent } from './payment/payment.component';
import { ReservationComponent } from './reservation/reservation.component';
import { GuestComponent } from './guest/guest.component';
import { OwnerDashboardComponent } from './owner-dashboard/owner-dashboard.component';
import { OwnerLoginComponent } from './owner-login/owner-login.component';
import { ManagerLoginComponent } from './manager-login/manager-login.component';
import { ManagerDashboardComponent } from './manager-dashboard/manager-dashboard.component';
import { ReceptionistLoginComponent } from './receptionist-login/receptionist-login.component';
import { ReceptionistDashboardComponent } from './receptionist-dashboard/receptionist-dashboard.component';

// const routes: Routes = [
//   {path:'dashboard',component:DashboardComponent},
//   { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
//   { path: 'login', component: LoginComponent },
//   // {
//   //   path:'Home', component:HomeComponent
//   // },
//   {
//     path:'owner-login',component:OwnerLoginComponent
//   },
//   {
//     path:'owner-dashboard',component:OwnerDashboardComponent
//   },
//   {
//     path:'manager-login',component:ManagerLoginComponent
//   },
//   {
//     path:'manager-dashboard',component:ManagerDashboardComponent
//   },
//   {
//     path:'staff', component:StaffComponent
//   },
//   {
//     path:'room', component:RoomComponent
//   },
//   {
//     path:'inventory',component:InventoryComponent
//   },
//   {
//     path:'receptionist-login',component:ReceptionistLoginComponent
//   },
//   {
//     path:'receptionist-dashboard',component:ReceptionistDashboardComponent 
//   },

//   {
//     path:'register',component:RegisterComponent
//   },
  
//   {
//     path:'payment',component:PaymentComponent
//   },
//   {
//     path:'reservation',component:ReservationComponent
//   },
//   {
//     path:'guest',component:GuestComponent
//   }

// ];

@NgModule({
  // imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
