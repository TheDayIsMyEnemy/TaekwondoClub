import { FC } from "react";
import { Table, ActionIcon, Group, Badge, Tooltip } from "@mantine/core";
import { ClubMembership, Student } from "../types";
import { Pencil, Trash } from "tabler-icons-react";

type StudentTableProps = {
  students: Student[];
  onRenewMembershipModalOpen: (selectedStudent: Student) => void;
  onDeleteStudentModalOpen: (selectedStudent: Student) => void;
};

export const StudentTable: FC<StudentTableProps> = ({
  students,
  onRenewMembershipModalOpen,
  onDeleteStudentModalOpen,
}): JSX.Element => {
  const getStatusBadge = (
    clubMembership: ClubMembership | null
  ): JSX.Element => {
    if (clubMembership == null) {
      return <></>;
    }

    const endDate = `${new Date(clubMembership.endDate).toLocaleDateString()}`;

    return clubMembership.isActive ? (
      <Tooltip label={endDate}>
        <Badge>Active</Badge>
      </Tooltip>
    ) : (
      <Tooltip label={endDate}>
        <Badge color="red">Expired</Badge>
      </Tooltip>
    );
  };

  return (
    <Table>
      <thead>
        <tr>
          <th>Id</th>
          <th>First Name</th>
          <th>Last Name</th>
          <th>Date Of Birth</th>
          <th>Phone</th>
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
                <td>{new Date(student.birthDate).toLocaleDateString()}</td>
                <td>{student.phoneNumber}</td>
                <td>{getStatusBadge(student.clubMembership)}</td>
                <td>
                  <Group spacing={0}>
                    <ActionIcon color="green"
                    onClick={() => {
                      onRenewMembershipModalOpen(student);
                    }}>
                      <Pencil size={16} />
                    </ActionIcon>
                    <ActionIcon color="red"
                    onClick={() => {
                      onDeleteStudentModalOpen(student);
                    }}>
                      <Trash size={16} />
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
