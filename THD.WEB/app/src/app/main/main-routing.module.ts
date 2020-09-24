import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { MainComponent } from './main.component';
import { AdminGuard } from '../shared/guards/admin.guard';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'pricing-simulation',
    pathMatch: 'full',
  },
  {
    path: '',
    component: MainComponent,
    children: [
      {
        canActivate: [AdminGuard],
        path: 'user-management', loadChildren: () => import('./user-management/user-management.module')
          .then(m => m.UserManagementModule)
      },
    ]
  },
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class MainRoutingModule { }
