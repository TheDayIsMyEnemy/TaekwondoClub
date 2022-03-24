type ClubMembership = {
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
  clubMembership: ClubMembership;
};

export type { ClubMembership, Student };
