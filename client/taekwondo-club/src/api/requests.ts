import client from ".";
import { CreateStudent } from "../types";

export const getStudents = () => {
  return client.get("/students");
};

export const createStudent = (request: CreateStudent) => {
  return client.post("/students", request);
};

export const deleteStudent = (id: number) => {
  return client.delete(`/students/${id}`);
};

export const getMemberships = () => {
  return client.get("/memberships");
};

export const createMembership = (
  studentId: number,
  startDate: Date | null,
  endDate: Date | null,
  subscriptionFee: number | undefined
) => {
  return client.post("/memberships", {
    studentId: studentId,
    startDate: startDate,
    endDate: endDate,
    subscriptionFee: subscriptionFee,
  });
};

export const updateMembership = (
  membershipId: number,
  startDate: Date,
  endDate: Date,
  subscriptionFee: number | undefined
) => {
  return client.put("/memberships", {
    membershipId: membershipId,
    startDate: startDate,
    endDate: endDate,
    subscriptionFee: subscriptionFee,
  });
};
