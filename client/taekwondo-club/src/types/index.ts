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
  clubMembership: ClubMembership | null;
};

type CreateStudentRequest = {
  firstName: string;
  lastName: string;
  gender: string;
  phoneNumber: string;
  birthDate: Date | null;
};

export type { Student, ClubMembership, CreateStudentRequest };
