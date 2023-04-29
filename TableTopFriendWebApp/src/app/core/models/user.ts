import { UserRole } from "../constants/userRole";
import { UserValidation } from "../constants/userValidation";

export interface User {
  id: string,
  firstName: string,
  lastName: string,
  email: string,
  image: string,
  userRole: UserRole,
  validation: UserValidation,
}


