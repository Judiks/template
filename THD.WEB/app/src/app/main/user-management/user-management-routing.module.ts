import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserManagementDashboardComponent } from './components/user-management-dashboard/user-management-dashboard.component';
import { UserManagementComponent } from './user-management.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'user-management-dashboard',
    pathMatch: 'full',
  },
  {
    path: '',
    component: UserManagementComponent,
    children: [
      {
        path: 'user-management-dashboard',
        component: UserManagementDashboardComponent
      }
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
export class UserManagementRoutingModule { }
