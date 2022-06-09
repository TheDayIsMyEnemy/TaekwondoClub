type Membership = {
  id: number;
  startDate: Date;
  endDate: Date;
  subscriptionFee: number;
  isActive: boolean;
  history: MembershipHistory[];
};

type MembershipHistory = {
  startDate: Date;
  endDate: Date;
  subscriptionFee: number;
};

type Student = {
  id: number;
  firstName: string;
  lastName: string;
  gender: string;
  phoneNumber: string;
  birthDate: Date;
  membership: Membership | null;
};

type CreateMembership = {
  startDate: Date | null;
  endDate: Date | null;
  subscriptionFee: number | undefined;
};

type CreateStudent = {
  firstName: string;
  lastName: string;
  gender: string;
  birthDate: Date | null | string;
  phoneNumber: string | null;
  membership: CreateMembership | null;
};

export type { Student, Membership, CreateMembership, CreateStudent };
