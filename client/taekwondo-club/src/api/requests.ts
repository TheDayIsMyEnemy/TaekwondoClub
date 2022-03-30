import client from ".";

export const getStudents = () => {
    return client.get("/students");
}

export const deleteStudent = (id: number) => {
    return client.delete(`/students/${id}`);
}

export const updateClubMembership = (
  clubMembershipId: number,
  startDate: Date,
  endDate: Date
) => {
  return client.put("/clubmembership", {
    clubMembershipId: clubMembershipId,
    startDate: startDate,
    endDate: endDate,
  });
};
