import { Membership } from "./membership";
import { Photo } from "./photo";
import { SupplementOrder } from "./SupplementOrder";

export interface Member {
    id: number;
    memberLoginName: string;
    firstName: string;
    middleName: string;
    lastName: string;
    dateOfBirth: Date;
    age: number;
    gender: number;
    email: string;
    password?: string | null;
    phoneNumber: string;
    alternatePhoneNumber: string;
    address: string;
    bloodGroup: string;
    joiningDate: Date;
    isOldmember: boolean;
    imageUrl: string;
    memberships: Membership[];
    supplementOrders: SupplementOrder[];
    photos:Photo[];
    token: string | null;
  }