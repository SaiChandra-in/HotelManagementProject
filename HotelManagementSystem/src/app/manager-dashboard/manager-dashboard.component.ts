import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';


@Component({
  selector: 'app-manager-dashboard',
  templateUrl: './manager-dashboard.component.html',
  styleUrl: './manager-dashboard.component.css'
})
export class ManagerDashboardComponent {
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
    // localStorage.removeItem('AdminName');
    // this.router.navigate(['/manager-login']);
    this.router.navigate(['/login']);
  }
}
