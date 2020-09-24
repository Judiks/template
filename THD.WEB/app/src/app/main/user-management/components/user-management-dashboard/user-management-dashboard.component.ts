import { ToastrService } from 'ngx-toastr';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { UserEditorComponent } from '../user-editor/user-editor.component';
import { UserRoleTypes } from 'src/app/shared/models/enum/user-role-type';
import { AreYouSurePopupComponent } from 'src/app/shared/components/are-you-sure-popup/are-you-sure-popup.component';
import { AuthenticationHelper } from 'src/app/shared/helpers/auth.helper';
import { AccountService } from 'src/app/swagger/shared/services';
import { UserResponse, CreateUserRequest, UpdateUserRequest } from 'src/app/swagger/shared/models';

@Component({
  selector: 'app-user-management-dashboard',
  templateUrl: './user-management-dashboard.component.html',
  styleUrls: ['./user-management-dashboard.component.scss']
})
export class UserManagementDashboardComponent implements OnInit {

  public users: UserResponse[];
  public usersTemp: UserResponse[];
  public rols = UserRoleTypes;
  private popupWidth = '465px';
  private popupHeight = '510px';
  public searchInput = '';

  constructor(private accountService: AccountService, public dialog: MatDialog, private toastr: ToastrService,
              private authenticationHelper: AuthenticationHelper) { }

  ngOnInit() {
    this.getAllUsers();
  }

  private getAllUsers(): void {
    this.accountService.AccountGetAll().subscribe(response => {
      this.users = response.users;
      this.usersTemp = response.users;
    });
  }

  public onCreateClick(): void {
    const dialogRef = this.dialog.open(UserEditorComponent, {
      width: this.popupWidth,
      height: this.popupHeight,
      data: null
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.createUser(result);
      }
    });
  }

  private createUser(newUser: CreateUserRequest): void {
    this.accountService.AccountCreate(newUser).subscribe(() => {
      this.toastr.success('User create success');
      this.getAllUsers();
    }, (error) => {
      this.toastr.error(error.error);
    });
  }

  private updateUser(updatedUser: UpdateUserRequest): void {
    this.accountService.AccountUpdate(updatedUser).subscribe(() => {
      this.toastr.success('User update success');
      this.getAllUsers();
    }, (error) => {
      this.toastr.error(error.error);
    });
  }

  public isMyself(id: string): boolean {
    return id === this.authenticationHelper.currentUserValue.id ? false : true;
  }

  public onRemoveClick(id: string): void {
    if (id === this.authenticationHelper.currentUserValue.id) {
      this.toastr.error('You can\'t remove yourself');
      return;
    }

    const dialogRef = this.dialog.open(AreYouSurePopupComponent, {
      width: '380px',
      height: '150px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.accountService.AccountDelete(id).subscribe(() => {
          this.toastr.success('User delete success');
          this.getAllUsers();
        }, (error) => {
          this.toastr.error(error.error);
        });
      }
    });

  }

  public onEditClick(userData: UserResponse) {
    const dialogRef = this.dialog.open(UserEditorComponent, {
      width: this.popupWidth,
      height: this.popupHeight,
      data: userData
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.updateUser(result);
      }
    });
  }

  public search(event: any): UserResponse[] {
    const searchInput = event.target.value;
    if (searchInput) {
      this.usersTemp = this.users.filter(x => x.userName.toLowerCase().includes(searchInput.toLowerCase()) ||
        x.firstName.toLowerCase().includes(searchInput.toLowerCase()) ||
        x.lastName.toLowerCase().includes(searchInput.toLowerCase()) ||
        this.rols[x.userRole].text.toLowerCase().includes(searchInput.toLowerCase()) ||
        x.email.toLowerCase().includes(searchInput.toLowerCase()));
      return;
    }
    this.usersTemp = this.users;
  }

}
