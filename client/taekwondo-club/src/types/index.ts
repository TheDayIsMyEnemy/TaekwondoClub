type Membership = {
  id: number;
  startDate: Date;
  endDate: Date;
  note: string;
  isActive: boolean;
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

type CreateStudent = {
  firstName: string;
  lastName: string;
  gender: string;
  phoneNumber: string;
  birthDate: Date | null;
  membershipPeriod: [Date | null, Date | null] | null;
};

export type { Student, Membership, CreateStudent };
