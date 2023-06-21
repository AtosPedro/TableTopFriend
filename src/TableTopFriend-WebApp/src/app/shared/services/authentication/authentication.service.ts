import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { AuthenticationResult } from './dtos/authenticationResult';
import { RegisterRequest } from './dtos/registerRequest';
import { LoginRequest } from './dtos/loginRequest';
import { Result } from 'src/app/core/models/result';

@Injectable({
  providedIn: 'root'
})

export class AuthenticationService {
  private serviceRoute = '/auth';
  private loginUrl = environment.apiUrl + this.serviceRoute + '/login';
  private registerUrl = environment.apiUrl + this.serviceRoute + '/register';

  constructor(private httpClient: HttpClient) { }

  public register(registerRequest: RegisterRequest): Result<AuthenticationResult, HttpErrorResponse> {
    let result: any = null;
    this.httpClient.post<AuthenticationResult>(this.registerUrl, registerRequest)
      .subscribe({
        next: value => result = Result.success<AuthenticationResult, HttpErrorResponse>(value),
        error: err => result = Result.fail<AuthenticationResult, HttpErrorResponse>(err)
      });

    return result;
  }

  public login(
    loginRequest: LoginRequest,
     succes: ( x : AuthenticationResult) => void,
     error: (err: any) => void) : void{

    let result: any = null;

    this.httpClient.post<AuthenticationResult>(this.loginUrl, loginRequest)
      .subscribe({
        next: value => succes(value),
        error: err => error(err.error)
      });
  }
}
