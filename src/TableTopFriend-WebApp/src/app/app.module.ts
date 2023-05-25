import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { AuthenticationModule } from './features/authentication/authentication.module';
import { HomeModule } from './features/home/home.module';
import { UsersModule } from './features/users/users.module';
import { CampaignsModule } from './features/campaigns/campaigns.module';
import { CharactersModule } from './features/characters/characters.module';
import { SessionsModule } from './features/sessions/sessions.module';
import { SkillsModule } from './features/skills/skills.module';
import { StatusModule } from './features/status/status.module';
import { RouterModule } from '@angular/router';
import { SharedModule } from './shared/shared.module';
import { CoreModule } from './core/core.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    RouterModule,
    SharedModule,
    CoreModule,
    AppRoutingModule,
    AuthenticationModule,
    CampaignsModule,
    CharactersModule,
    HomeModule,
    SessionsModule,
    SkillsModule,
    StatusModule,
    UsersModule  
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
