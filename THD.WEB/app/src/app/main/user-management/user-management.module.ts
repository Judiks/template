import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { SharedModule } from 'src/app/shared/shared.module';
import { UserEditorComponent } from './components/user-editor/user-editor.component';
import { UserManagementDashboardComponent } from './components/user-management-dashboard/user-management-dashboard.component';
import { UserManagementRoutingModule } from './user-management-routing.module';
import { UserManagementComponent } from './user-management.component';

@NgModule({
  declarations: [
    UserManagementComponent,
    UserManagementDashboardComponent,
    UserEditorComponent,
  ],
  entryComponents: [
    UserEditorComponent,
  ],
  imports: [
    FormsModule,
    CommonModule,
    UserManagementRoutingModule,
    MatDialogModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    SharedModule
  ]
})
export class UserManagementModule { }
