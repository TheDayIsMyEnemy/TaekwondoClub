type ClubMembership = {
  id: number;
  startDate: Date | null;
  endDate: Date | null;
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
  clubMembership: ClubMembership | null;
};

export type { ClubMembership, Student };
