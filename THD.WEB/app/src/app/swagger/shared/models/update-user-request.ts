/* tslint:disable */
import { UserRoleModel } from './user-role-model';
export interface UpdateUserRequest {
  id: string;
  userName: string;
  email: string;
  firstName: string;
  lastName: string;
  password: string;
  userRole: UserRoleModel;
}
