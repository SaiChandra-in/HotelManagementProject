import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-receptionist-dashboard',
  templateUrl: './receptionist-dashboard.component.html',
  styleUrl: './receptionist-dashboard.component.css'
})
export class ReceptionistDashboardComponent {
  constructor(private http: HttpClient, private router: Router) {}
  s = localStorage.getItem('adminName');

  //-->
  images: string[] = ['/assets/ManagerBackground.jpg', '/assets/Background3.jpg','/assets/Background4.jpg','/assets/Background5.jpg','/assets/Background6.jpg'];
  currentImageIndex: number = 0;
  imageUrl: string = this.images[this.currentImageIndex];

  ngOnInit() {
    setInterval(() => {
      this.currentImageIndex = (this.currentImageIndex + 1) % this.images.length;
      this.imageUrl = this.images[this.currentImageIndex];
    }, 5000); // Change image every 20 seconds
  }
//<--

  logOut(){
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    localStorage.removeItem('adminName');
    // this.router.navigate(['/receptionist-login']);
    this.router.navigate(['/login']);
  }
}
