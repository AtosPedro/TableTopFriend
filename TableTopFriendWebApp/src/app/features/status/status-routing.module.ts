import { NgModule } from '@angular/core';
import { ListComponent } from './pages/list/list.component';
import { RouterModule, Routes } from '@angular/router';


const routes: Routes = [
  { path: 'statuses', component: ListComponent },
]

@NgModule({
  imports:[RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class StatusRoutingModule { }
