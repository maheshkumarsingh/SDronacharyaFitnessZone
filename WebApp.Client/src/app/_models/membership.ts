export interface Membership {
    readonly id: number;
    membershipType: number;
    isMembershipActive: boolean;
    membershipStartDate: string;
    membershipEndDate: string;
    membershipAmount: number;
    memberLoginName: string;
    paidAmount: number;
    dueAmount: number;
  }