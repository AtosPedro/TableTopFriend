import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListComponent } from './pages/list/list.component';

const routes: Routes = [
  { path: 'characters', component: ListComponent },
]

@NgModule({
  imports:[RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class CharactersRoutingModule { }
