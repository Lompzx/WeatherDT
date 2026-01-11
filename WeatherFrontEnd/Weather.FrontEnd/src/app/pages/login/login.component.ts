import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';
import { LoginRequest } from '../../core/models/auth.model';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router'; 
import { ReactiveFormsModule } from '@angular/forms';
import { AppError } from '../../core/models/app-error.model';
import { LoginResponse } from '../../core/models/auth-response.model';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
   imports: [CommonModule, RouterModule, ReactiveFormsModule],
})
export class LoginComponent implements OnInit {
  loginResponse?: LoginResponse;  
  loginError?: AppError;
  loginForm!: FormGroup;
  showPassword = false;
  isLoading = false;  

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.checkRememberedUser();
  }

  private initForm(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      rememberMe: [false]
    });
  }

  private checkRememberedUser(): void {
    const rememberedEmail = localStorage.getItem('rememberedEmail');
    if (rememberedEmail) {
      this.loginForm.patchValue({
        email: rememberedEmail,
        rememberMe: true
      });
    }
  }

  togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }

 async onSubmit(): Promise<void> {
  if (this.loginForm.invalid) {
    this.markFormGroupTouched(this.loginForm);
    return;
  }

  this.isLoading = true;
  this.loginError = undefined;

  const loginRequest: LoginRequest = {
    email: this.loginForm.value.email,
    password: this.loginForm.value.password,
    twoFactorCode: '',
    twoFactorRecoveryCode: ''
  };

  try {
    const response = await this.authService.postLogin(loginRequest);
    this.loginResponse = response;
    
    if (this.loginForm.value.rememberMe) {
      localStorage.setItem('rememberedEmail', loginRequest.email);
    } else {
      localStorage.removeItem('rememberedEmail');
    }

    this.router.navigate(['/forecast']);
  } catch (err) {
    this.loginError = err as AppError;      
  } finally {
    this.isLoading = false;
    this.cdr.detectChanges();
  }
}

  private markFormGroupTouched(formGroup: FormGroup): void {
    Object.values(formGroup.controls).forEach(control => {
      control.markAsTouched();
      if (control instanceof FormGroup) {
        this.markFormGroupTouched(control);
      }
    });
  }
}
