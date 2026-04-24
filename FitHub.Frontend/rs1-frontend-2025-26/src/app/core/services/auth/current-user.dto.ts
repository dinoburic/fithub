export interface CurrentUserDto {
  userId: number;
  email: string;
  isAdmin: boolean | 'false';
  isManager: boolean | 'false';
  isEmployee: boolean | 'false';
  tokenVersion: number;
}
