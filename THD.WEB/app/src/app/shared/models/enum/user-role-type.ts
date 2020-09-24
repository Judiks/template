export enum UserRoleType {
    SuperAdmin = 0,
    Admin = 1,
    User = 2
}

export const UserRoleTypes = Object.values(UserRoleType)
  .filter(value => typeof(value) === 'number')
  .map(value => ({ value, text: UserRoleType[value].toString() }));
