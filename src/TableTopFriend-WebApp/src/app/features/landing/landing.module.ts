import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LandingComponent } from './pages/landing/landing.component';
import { LandingRoutingModule } from './landing-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { CharacterExplanationComponent } from './pages/character-explanation/character-explanation.component';
import { CampaignExplanationComponent } from './pages/campaign-explanation/campaign-explanation.component';
import { AudioEffectExplanationComponent } from './pages/audio-effect-explanation/audio-effect-explanation.component';
import { LandingNavigationComponent } from './landing-navigation/landing-navigation.component';



@NgModule({
  declarations: [
    LandingComponent,
    CharacterExplanationComponent,
    CampaignExplanationComponent,
    AudioEffectExplanationComponent,
    LandingNavigationComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    LandingRoutingModule
  ]
})
export class LandingModule { }
