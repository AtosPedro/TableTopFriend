import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthenticationResult } from './dtos/authenticationResult';
import { RegisterRequest } from './dtos/registerRequest';
import { LoginRequest } from './dtos/loginRequest';

@Injectable({
  providedIn: 'root'
})

export class AuthenticationService {
  private loginUrl = environment.apiUrl + '/login';
  private registerUrl = environment.apiUrl + '/register';

  constructor(private httpClient: HttpClient) { }

  public register(registerRequest: RegisterRequest): AuthenticationResult | null {
    let authenticationResult: AuthenticationResult | null = null;
    this.httpClient.post<AuthenticationResult>(this.registerUrl, registerRequest)
      .subscribe(response => {
        authenticationResult = response;
      });
    return authenticationResult;
  }

  public login(loginRequest: LoginRequest): AuthenticationResult | null {
    let authenticationResult: AuthenticationResult | null = null;
    this.httpClient.post<AuthenticationResult>(this.loginUrl, loginRequest)
      .subscribe(response => {
        authenticationResult = response;
      });
    return authenticationResult;
  }
}
