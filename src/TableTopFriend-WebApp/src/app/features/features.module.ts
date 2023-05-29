import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthenticationModule } from './authentication/authentication.module';
import { CampaignsModule } from './campaigns/campaigns.module';
import { CharactersModule } from './characters/characters.module';
import { HomeModule } from './home/home.module';
import { SessionsModule } from './sessions/sessions.module';
import { SkillsModule } from './skills/skills.module';
import { StatusModule } from './status/status.module';
import { UsersModule } from './users/users.module';
import { LandingModule } from './landing/landing.module';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    AuthenticationModule,
    CampaignsModule,
    CharactersModule,
    HomeModule,
    LandingModule,
    SessionsModule,
    SkillsModule,
    StatusModule,
    UsersModule,
  ]
})
export class FeaturesModule { }
