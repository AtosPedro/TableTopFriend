import { Component } from '@angular/core';
import { AuthenticationService } from 'src/app/shared/services/authentication/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  constructor(private authenticationService: AuthenticationService) { }
}
