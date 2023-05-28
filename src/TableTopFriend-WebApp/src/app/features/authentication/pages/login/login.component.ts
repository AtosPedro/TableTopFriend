import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthenticationService } from 'src/app/shared/services/authentication/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  constructor(
    private authenticationService: AuthenticationService,
    private builder: FormBuilder) { }
  hide = true;

  loginForm = this.builder.group({
    email: this.builder.control('',Validators.compose([Validators.required, Validators.minLength(5)])),
    password: this.builder.control('', Validators.compose([Validators.required, Validators.minLength(8)]))
  })

  login() {
    const LoginRequest = {
      email: this.loginForm.value.email || '',
      password: this.loginForm.value.password || ''
    }
    this.authenticationService.login(LoginRequest);
  }
}
