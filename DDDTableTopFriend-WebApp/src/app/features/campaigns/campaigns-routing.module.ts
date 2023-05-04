import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListComponent } from './pages/list/list.component';

const routes: Routes = [
  { path: 'campaigns', component: ListComponent },
]

@NgModule({
  imports:[RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class CampaignsRoutingModule { }