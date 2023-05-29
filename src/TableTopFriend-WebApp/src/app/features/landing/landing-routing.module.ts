import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LandingComponent } from './pages/landing/landing.component';
import { AudioEffectExplanationComponent } from './pages/audio-effect-explanation/audio-effect-explanation.component';
import { CharacterExplanationComponent } from './pages/character-explanation/character-explanation.component';
import { CampaignExplanationComponent } from './pages/campaign-explanation/campaign-explanation.component';


const ROUTES: Routes = [
  { path: '', component: LandingComponent },
  { path: 'audio-effect-ex', component: AudioEffectExplanationComponent },
  { path: 'character-ex', component: CharacterExplanationComponent },
  { path: 'campaign-ex', component: CampaignExplanationComponent }
]

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(ROUTES)],
  exports: [RouterModule]
})
export class LandingRoutingModule { }
