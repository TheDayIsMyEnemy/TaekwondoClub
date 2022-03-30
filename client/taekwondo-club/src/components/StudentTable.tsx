import { FC } from "react";
import { Table, ActionIcon, Group } from "@mantine/core";
import { Student } from "../types";
import { PencilIcon, DiffRemovedIcon } from "@primer/octicons-react";

type StudentTableProps = {
  students: Student[];
  onMembershipModalOpen: (selectedStudent: Student) => void;
};

export const StudentTable: FC<StudentTableProps> = ({
  students,
  onMembershipModalOpen,
}): JSX.Element => {
  return (
    <Table>
      <thead>
        <tr>
          <th>Id</th>
          <th>First Name</th>
          <th>Last Name</th>
          <th>Gender</th>
          <th>Date Of Birth</th>
          <th>Membership</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        {students &&
          students.map((student) => {
            return (
              <tr key={student.id}>
                <td>{student.id}</td>
                <td>{student.firstName}</td>
                <td>{student.lastName}</td>
                <td>{student.gender}</td>
                <td>{new Date(student.birthDate).toLocaleDateString()}</td>
                <td>
                  {student.clubMembership?.isActive
                    ? `Active until ${new Date(
                        student.clubMembership.endDate!
                      ).toLocaleDateString()}`
                    : student.clubMembership?.endDate != null
                    ? `Expired on ${student.clubMembership.endDate}`
                    : `Not created`}
                </td>
                <td>
                  <Group spacing={8}>
                    <ActionIcon
                      variant="light"
                      onClick={() => {
                        onMembershipModalOpen(student);
                      }}
                    >
                      <PencilIcon />
                    </ActionIcon>
                    <ActionIcon variant="light">
                      <DiffRemovedIcon />
                    </ActionIcon>
                  </Group>
                </td>
              </tr>
            );
          })}
      </tbody>
    </Table>
  );
};
