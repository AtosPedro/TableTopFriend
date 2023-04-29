import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormComponent } from './pages/form/form.component';
import { ListComponent } from './pages/list/list.component';
import { SkillsRoutingModule } from './skills-routing.module';

@NgModule({
  declarations: [
    FormComponent,
    ListComponent
  ],
  imports: [
    CommonModule,
    SkillsRoutingModule
  ]
})

export class SkillsModule { }
