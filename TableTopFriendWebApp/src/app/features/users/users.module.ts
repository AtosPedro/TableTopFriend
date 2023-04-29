import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfileComponent } from './pages/profile/profile.component';
import { UsersRoutingModule } from './users-routing.module';

@NgModule({
  declarations: [
    ProfileComponent
  ],
  imports: [
    CommonModule,
    UsersRoutingModule
  ]
})

export class UsersModule { }
