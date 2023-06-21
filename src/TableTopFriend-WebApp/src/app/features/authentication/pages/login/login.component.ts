import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/shared/services/authentication/authentication.service';
import { LoginRequest } from 'src/app/shared/services/authentication/dtos/loginRequest';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  constructor(
    private authenticationService: AuthenticationService,
    private builder: FormBuilder,
    private snackBar: MatSnackBar,
    private router: Router) { }
  hide = true;

  loginForm = this.builder.group({
    email: this.builder.control('', Validators.compose([Validators.required, Validators.minLength(5)])),
    password: this.builder.control('', Validators.compose([Validators.required, Validators.minLength(8)]))
  })

  login() {

    if (this.loginForm.invalid) {
      return;
    }

    const LoginRequest: LoginRequest = {
      email: this.loginForm.value.email || '',
      password: this.loginForm.value.password || ''
    };

    this.authenticationService.login(
      LoginRequest,
      (result) => {
        this.snackBar.open('Welcome ' + result.firstName + ' ' + result.lastName || '', 'Close', {
          duration: 10000,
          verticalPosition: 'top'
        });

        this.router.navigate(['/home']);
      },
      (err) => {        
        this.snackBar.open(err.title || '', 'Close', {
          duration: 10000,
          verticalPosition: 'top'
        });
        
        this.loginForm.controls['password'].setErrors({'incorrect': true});
        this.loginForm.controls['email'].setErrors({'incorrect': true});
      }
    );
  }
}
