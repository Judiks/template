/* tslint:disable */
import { UserRoleModel } from './user-role-model';
export interface CreateUserRequest {
  userName: string;
  email: string;
  firstName: string;
  lastName: string;
  password: string;
  userRole: UserRoleModel;
}
