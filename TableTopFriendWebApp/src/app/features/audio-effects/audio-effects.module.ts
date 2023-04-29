import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormComponent } from './pages/form/form.component';
import { ListComponent } from './pages/list/list.component';
import { AudioEffectsRoutingModule } from './audio-effects-routing.module';

@NgModule({
  declarations: [
    FormComponent,
    ListComponent
  ],
  imports: [
    CommonModule,
    AudioEffectsRoutingModule
  ]
})

export class AudioEffectsModule { }
