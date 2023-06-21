import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { UserRole } from 'src/app/core/constants/userRole';
import { AuthenticationService } from 'src/app/shared/services/authentication/authentication.service';
import { RegisterRequest } from 'src/app/shared/services/authentication/dtos/registerRequest';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  hide = true;

  constructor(
    private authenticationService: AuthenticationService,
    private builder: FormBuilder) { }

  registerForm = this.builder.group({
    firstName: this.builder.control('', Validators.compose([Validators.required])),
    lastName:this.builder.control('', Validators.compose([Validators.required])),
    email:this.builder.control('', Validators.compose([Validators.required, Validators.email])),
    password:this.builder.control('', Validators.compose([Validators.required])),
    passwordConfirmation:this.builder.control('', Validators.compose([Validators.required])),
  });

  register() {

    if(this.registerForm.value.passwordConfirmation !== this.registerForm.value.password)
      this.registerForm.controls['passwordConfirmation'].setErrors({'incorrect': true});

    let registerRequest : RegisterRequest = {
      firstName : this.registerForm.value.firstName || '',
      lastName : this.registerForm.value.lastName || '',
      email : this.registerForm.value.email || '',
      password : this.registerForm.value.password || '',
      role: UserRole.freeUser
    };

    let result = this.authenticationService.register(registerRequest);
  }
}
