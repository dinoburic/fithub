// users-api.model.ts

/**
 * DTO for fetching user data (maps to GetUserByIdQueryDto.cs)
 */
export interface GetUserByIdQueryDto {
  id: number;
  name: string;
  surname: string;
  centerID: number;
  gender: string;      // Backend returns "Male" or "Female" string
  email: string;
  status: string;
  address: string;
  phoneNumber: string;
  roleID: number;
  weight?: number | null;
  height?: number | null;
}

/**
 * Command for updating a user (maps to UpdateUserCommand.cs)
 */
export interface UpdateUserCommand {
  // id: number; -> Do not send in body because it goes in URL route, and backend has [JsonIgnore]
  name?: string | null;
  surname?: string | null;
  centerID?: number | null;
  gender?: boolean | null; // Note: Here it is boolean in Update command, but string in Get command
  email?: string | null;
  password?: string | null;
  status?: string | null;
  address?: string | null;
  phoneNumber?: string | null;
  roleID?: number | null;
  badgeID?: number | null;
  weight?: number | null;
  height?: number | null;
  fitnessPlanTypeID?: number | null;
}
