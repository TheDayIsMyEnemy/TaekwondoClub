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

export const createMembership = (
  studentId: number,
  startDate: Date,
  endDate: Date
) => {
  return client.post("/membership", {
    studentId: studentId,
    startDate: startDate,
    endDate: endDate,
  });
};

export const updateMembership = (
  membershipId: number,
  startDate: Date,
  endDate: Date
) => {
  return client.put("/membership", {
    membershipId: membershipId,
    startDate: startDate,
    endDate: endDate,
  });
};
