import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { AuthenticationResult } from './dtos/authenticationResult';
import { RegisterRequest } from './dtos/registerRequest';
import { LoginRequest } from './dtos/loginRequest';

@Injectable({
  providedIn: 'root'
})

export class AuthenticationService {
  private loginUrl = environment.apiUrl + '/auth/login';
  private registerUrl = environment.apiUrl + '/auth/register';

  constructor(private httpClient: HttpClient) { }

  public register(registerRequest: RegisterRequest): AuthenticationResult | any {
    let result: AuthenticationResult | any = null;

    this.httpClient.post<AuthenticationResult>(this.registerUrl, registerRequest)
      .subscribe({
        next: value => result = value,
        error: err => result = err
      });

    return result;
  }

  public login(loginRequest: LoginRequest): AuthenticationResult | any {
    let result: AuthenticationResult | any = null;

    this.httpClient.post<AuthenticationResult>(this.loginUrl, loginRequest)
      .subscribe({
        next: value => result = value,
        error: err => result = err
      });

    return result;
  }

}
