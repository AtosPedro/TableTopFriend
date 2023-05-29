import { UserRole } from "src/app/core/constants/userRole";

export type RegisterRequest = {
   firstName : string,
   lastName : string,
   email : string,
   password : string,
   role: UserRole
}
