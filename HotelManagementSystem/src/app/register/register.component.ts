import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RegisterService } from '../register.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  registrationForm: FormGroup;

  constructor(private fb: FormBuilder, private guestService: RegisterService) {
    this.registrationForm = this.fb.group({
      GuestName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(20)]],
      GuestEmail: ['', [Validators.required, Validators.email]],
      Gender: ['', [Validators.required]],
      Address: ['', [Validators.maxLength(200)]],
      PhoneNo: [''],
      Password: ['', [Validators.required]],
      ConfirmPassword: ['', [Validators.required]]
    }, { validators: this.passwordMatchValidator });
  }

  get f() { return this.registrationForm.controls; }

  onSubmit() {
    if (this.registrationForm.invalid) {
      return;
    }

    this.guestService.registerGuest(this.registrationForm.value).subscribe(
      (      response: any) => {
        console.log('Registration successful', response);
        alert('Registration Successful');
      },
      error => {
        console.error('Registration error', error);
      }
    );
  }

  private passwordMatchValidator(form: FormGroup) {
    return form.get('Password')?.value === form.get('ConfirmPassword')?.value
      ? null : { mismatch: true };
  }
}
