import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormGroup, FormControl, Validators, ValidationErrors } from '@angular/forms';
import { UserRoleTypes } from 'src/app/shared/models/enum/user-role-type';
import { GetAllUserResponse, UserResponse, CreateUserRequest, UpdateUserRequest } from 'src/app/swagger/shared/models';

@Component({
  selector: 'app-user-editor',
  templateUrl: './user-editor.component.html',
  styleUrls: ['./user-editor.component.scss']
})
export class UserEditorComponent implements OnInit {

  public editUserForm: FormGroup;
  public rolDropdownForm: FormGroup;
  public isNewUser: boolean;
  public rols = UserRoleTypes;

  constructor(public dialogRef: MatDialogRef<UserEditorComponent>, @Inject(MAT_DIALOG_DATA) public inputData: UserResponse) {
  }

  ngOnInit() {
    this.isNewUser = this.inputData ? false : true;
    this.initializeEditUserForm(this.inputData);
  }

  private initializeEditUserForm(user: UserResponse): void {
    this.editUserForm = new FormGroup({
      id: new FormControl(user ? user.id : ''),
      userName: new FormControl(user ? user.userName : '', Validators.required),
      firstName: new FormControl(user ? user.firstName : '', Validators.required),
      lastName: new FormControl(user ? user.lastName : '', Validators.required),
      role: new FormControl(user ? this.rols.find(x => x.value === user.userRole) : this.rols.find(x => x.value === 2), Validators.required),
      email: new FormControl(user ? user.email : '', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, this.passwordValidator])
    });
  }


  public onCreateClick(): void {
    if (this.editUserForm.valid) {
      const user: CreateUserRequest = {
        userName: this.editUserForm.controls.userName.value,
        firstName: this.editUserForm.controls.firstName.value,
        lastName: this.editUserForm.controls.lastName.value,
        userRole: this.editUserForm.controls.role.value.value,
        email: this.editUserForm.controls.email.value,
        password: this.editUserForm.controls.password.value,
    }
      this.dialogRef.close(user);
    }
  }

  public onUpdateClick(): void {
    if (this.editUserForm.valid) {
      const user: UpdateUserRequest = {
        id: this.editUserForm.controls.id.value,
        userName: this.editUserForm.controls.userName.value,
        firstName: this.editUserForm.controls.firstName.value,
        lastName: this.editUserForm.controls.lastName.value,
        userRole: this.editUserForm.controls.role.value.value,
        email: this.editUserForm.controls.email.value,
        password: this.editUserForm.controls.password.value,
      }
      this.dialogRef.close(user);
    }
  }

  public onExitClick(): void {
    this.dialogRef.close();
  }

  private passwordValidator(control: FormControl): ValidationErrors {
    const password = control.value;
    if (password.length < 8 || password.length > 20) {
      return { error: 'Must be atleast 8-20 characters' };
    }
    if (!/[0-9]/.test(password)) {
      return { error: 'Password must have number' };
    }
    if (!/[A-Z]/.test(password)) {
      return { error: 'Must contain capital letters' };
    }
    return null;
  }

}
