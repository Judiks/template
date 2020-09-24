/* tslint:disable */
import { UserRoleModel } from './user-role-model';
export interface UserResponse {
  id?: string;
  userName?: string;
  email?: string;
  firstName?: string;
  lastName?: string;
  userRole: UserRoleModel;
}
