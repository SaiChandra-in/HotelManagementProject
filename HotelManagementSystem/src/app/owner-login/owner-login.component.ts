import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-owner-login',
  templateUrl: './owner-login.component.html',
  styleUrls: ['./owner-login.component.css']
})
export class OwnerLoginComponent{
  loginData = {
    username: '',
    password: ''
  };
  errorMessage: string | null = null;

  constructor(private http: HttpClient, private router: Router) {}

  onSubmit(): void {
    this.http.post<{ token: string, admin: any }>('http://localhost:5126/api/Admins/Login', this.loginData)
      .subscribe({
        next: (response) => {
          localStorage.setItem('token', response.token);
          localStorage.setItem('role', response.admin.role);
          localStorage.setItem('adminName',response.admin.adminName);
          // Optionally store admin details in local storage or a service
          // localStorage.setItem('admin', JSON.stringify(response.admin));
          if(response.admin.role=="Owner"){
            this.router.navigate(['/owner-dashboard']); // Redirect to a different route upon successful login
          }
          else{
            alert('You must be Owner to Login');
          }
          // this.router.navigate(['/manager-dashboard']); // Redirect to a different route upon successful login
          sessionStorage.setItem('token', response.token);
        },
        error: (err) => {
          this.errorMessage = err.error.message || 'Login failed';
        }
      });
  }
}
