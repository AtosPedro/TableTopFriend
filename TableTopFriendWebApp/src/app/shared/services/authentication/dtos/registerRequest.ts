import { UserRole } from "src/app/core/constants/userRole";

export interface RegisterRequest{
   firstName : string,
   lastName : string,
   email : string,
   password : string,
   role: UserRole
}
