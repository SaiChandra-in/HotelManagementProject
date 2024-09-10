import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginData = {
    username: '',
    password: '',
    role:''
  };
  roles=['Owner','Manager','Receptionist']
  errorMessage: string | null = null;

  constructor(private http: HttpClient, private router: Router) {}

  onSubmit(): void {
    this.http.post<{ token: string, admin: any }>('http://localhost:5126/api/Admins/Login', this.loginData)
      .subscribe({
        next: (response) => {

          if (response.admin.role !== this.loginData.role) {
            alert('Role mismatch. Please select the correct role.');
            // this.errorMessage = 'Role mismatch. Please select the correct role.';
            return;
          }


          localStorage.setItem('token', response.token);
          localStorage.setItem('role', response.admin.role);
          localStorage.setItem('adminName',response.admin.adminName);
          
          if(response.admin.role==="Manager"){
            this.router.navigate(['/manager-dashboard']); // Redirect to a different route upon successful login
          }
          else if(response.admin.role=="Owner"){
            this.router.navigate(['/owner-dashboard']);
          }
          else if(response.admin.role=="Receptionist"){
            this.router.navigate(['/receptionist-dashboard']);
          }
          else{
            alert('Invalid Login');
          }
          // sessionStorage.setItem('token', response.token);
        },
        error: (err) => {
          this.errorMessage = err.error.message || 'Login failed';
        }
      });
  }
}
