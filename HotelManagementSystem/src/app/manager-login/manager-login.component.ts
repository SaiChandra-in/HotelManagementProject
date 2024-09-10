import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-manager-login',
  templateUrl: './manager-login.component.html',
  styleUrl: './manager-login.component.css'
})
export class ManagerLoginComponent {
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
          if(response.admin.role=="Manager"){
            this.router.navigate(['/manager-dashboard']); // Redirect to a different route upon successful login
          }
          else{
            alert('You must be Manager to Login');
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
