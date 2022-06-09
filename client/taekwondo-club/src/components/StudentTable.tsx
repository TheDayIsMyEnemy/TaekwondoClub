import { FC } from "react";
import {
  Table,
  ActionIcon,
  Group,
  Badge,
  Tooltip,
  ScrollArea,
} from "@mantine/core";
import { Membership, Student } from "../types";
import { Pencil, Trash } from "tabler-icons-react";
import { IdBadgeIcon } from "@primer/octicons-react";
import { useMediaQuery } from "@mantine/hooks";

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
  const getStatusBadge = (membership: Membership | null): JSX.Element => {
    if (membership == null) {
      return <></>;
    }

    const endDate = `${new Date(membership.endDate).toLocaleDateString()}`;

    return membership.isActive ? (
      <Tooltip label={endDate}>
        <Badge>Active</Badge>
      </Tooltip>
    ) : (
      <Tooltip label={endDate}>
        <Badge color="red">Expired</Badge>
      </Tooltip>
    );
  };
  const matchesBreakpoint = useMediaQuery("(min-width: 900px)");
  return (
    <ScrollArea>
      <Table
        sx={{ minWidth: 800 }}
        fontSize={matchesBreakpoint ? "lg" : "xs"}
        horizontalSpacing="xs"
        verticalSpacing="xs"
      >
        <thead>
          <tr>
            <th>FirstName</th>
            <th>LastName</th>
            <th>BirthDate</th>
            <th>Phone No.</th>
            <th>Membership</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {students &&
            students.map((student) => {
              return (
                <tr key={student.id}>
                  <td>{student.firstName}</td>
                  <td>{student.lastName}</td>
                  <td>
                    {student.birthDate
                      ? new Date(student.birthDate).toLocaleDateString()
                      : null}
                  </td>
                  <td>{student.phoneNumber}</td>
                  <td>{getStatusBadge(student.membership)}</td>
                  <td>
                    <Group spacing={0}>
                      <ActionIcon
                        color="blue"
                        onClick={() => {
                          onRenewMembershipModalOpen(student);
                        }}
                      >
                        <IdBadgeIcon size={18} />
                      </ActionIcon>
                      <ActionIcon color="green">
                        <Pencil size={20} />
                      </ActionIcon>
                      <ActionIcon
                        color="red"
                        onClick={() => {
                          onDeleteStudentModalOpen(student);
                        }}
                      >
                        <Trash size={20} />
                      </ActionIcon>
                    </Group>
                  </td>
                </tr>
              );
            })}
        </tbody>
      </Table>
    </ScrollArea>
  );
};
