import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormComponent } from './pages/form/form.component';
import { ListComponent } from './pages/list/list.component';
import { SheetComponent } from './pages/sheet/sheet.component';
import { CharactersRoutingModule } from './characters-routing.module';

@NgModule({
  declarations: [
    FormComponent,
    ListComponent,
    SheetComponent
  ],
  imports: [
    CommonModule,
    CharactersRoutingModule
  ]
})

export class CharactersModule { }
