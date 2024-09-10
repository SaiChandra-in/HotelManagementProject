import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { GuestComponent } from './guest/guest.component';
import { InventoryComponent } from './inventory/inventory.component';
import { PaymentComponent } from './payment/payment.component';
import { ReservationComponent } from './reservation/reservation.component';
import { RoomComponent } from './room/room.component';
import { StaffComponent } from './staff/staff.component';
import { RouteComponent } from './route/route.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthInterceptor } from './auth.interceptor';
import { OwnerLoginComponent } from './owner-login/owner-login.component';
import { ManagerLoginComponent } from './manager-login/manager-login.component';
import { ReceptionistLoginComponent } from './receptionist-login/receptionist-login.component';
import { OwnerDashboardComponent } from './owner-dashboard/owner-dashboard.component';
import { ReceptionistDashboardComponent } from './receptionist-dashboard/receptionist-dashboard.component';
import { ManagerDashboardComponent } from './manager-dashboard/manager-dashboard.component';
import { RouterModule, Routes } from '@angular/router';
import { PaymentHistoryComponent } from './payment-history/payment-history.component';
import { AuthGuard } from './auth.guard';


const routes: Routes = [
  {path:'dashboard',component:DashboardComponent},
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  // {
  //   path:'Home', component:HomeComponent
  // },
  // {
  //   path:'owner-login',component:OwnerLoginComponent
  // },
  {
    path:'owner-dashboard',component:OwnerDashboardComponent,canActivate: [AuthGuard], data: { role: 'Owner' }
  },
  // {
  //   path:'manager-login',component:ManagerLoginComponent
  // },
  {
    path:'manager-dashboard', component:ManagerDashboardComponent,canActivate: [AuthGuard], data: { role: 'Manager' }
  },
  {
    path:'staff', component:StaffComponent,canActivate: [AuthGuard], data: { role: 'Manager' }
  },
  {
    path:'room', component:RoomComponent,canActivate: [AuthGuard], data: { role: 'Manager' }
  },
  {
    path:'inventory',component:InventoryComponent,canActivate: [AuthGuard], data: { role: 'Manager' }
  },
  // {
  //   path:'receptionist-login',component:ReceptionistLoginComponent
  // },
  {
    path:'receptionist-dashboard',component:ReceptionistDashboardComponent,canActivate: [AuthGuard], data: { role: 'Receptionist' } 
  },

  {
    path:'register',component:RegisterComponent
  },
  
  {
    path:'payment',component:PaymentComponent,canActivate: [AuthGuard], data: { role: 'Receptionist' } 
  },
  {
    path:'reservation',component:ReservationComponent,canActivate: [AuthGuard], data: { role: 'Receptionist' } 
  },
  {
    path:'guest',component:GuestComponent,canActivate: [AuthGuard], data: { role: 'Receptionist' } 
  },
  {
    path:'PayHistory',component:PaymentHistoryComponent,canActivate: [AuthGuard], data: { role: 'Owner' }
  }
  // },
  // {
  //   path:'**',component:DashboardComponent
  // }

];


@NgModule({
  declarations: [
    AppComponent,
    GuestComponent,
    InventoryComponent,
    PaymentComponent,
    ReservationComponent,
    RoomComponent,
    StaffComponent,
    RouteComponent,
    RegisterComponent,
    LoginComponent,
    HomeComponent,
    DashboardComponent,
    OwnerLoginComponent,
    ManagerLoginComponent,
    ReceptionistLoginComponent,
    OwnerDashboardComponent,
    ReceptionistDashboardComponent,
    ManagerDashboardComponent,
    PaymentHistoryComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(routes)
  ],
  providers: [
    provideClientHydration(),
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
