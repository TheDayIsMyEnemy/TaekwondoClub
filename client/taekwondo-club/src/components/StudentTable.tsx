import React, { FC, useState } from "react";
import {
  Table,
  Button,
  ActionIcon,
  Group,
  Modal,
  Space,
  Center,
} from "@mantine/core";
import { RangeCalendar } from "@mantine/dates";
import { Student } from "../types";
import { PencilIcon, DiffRemovedIcon } from "@primer/octicons-react";
import dayjs from "dayjs";
import client from "../api";

type StudentTableProps = {
  students: Student[];
  onUpdateClubMembership: (
    clubMembershipId: number,
    startDate: Date,
    endDate: Date
  ) => void;
};

export const StudentTable: FC<StudentTableProps> = ({
  students,
  onUpdateClubMembership,
}): JSX.Element => {
  const [membershipModal, setMembershipModal] = useState(false);
  const [membershipCalendar, setMembershipCalendar] = useState<
    [Date | null, Date | null]
  >([null, null]);
  const [student, setStudent] = useState<Student | undefined>();

  const createClubMembership = () => {};

  const updateClubMembership = () => {};

  return (
    <>
      <Modal
        size="xl"
        opened={membershipModal}
        onClose={() => setMembershipModal(false)}
        title="Set Membership"
      >
        <form
          onSubmit={(e) => {
            e.preventDefault();
            if (student?.clubMembership) {
              setMembershipModal(false);
              onUpdateClubMembership(
                student.clubMembership.id,
                membershipCalendar[0] as Date,
                membershipCalendar[1] as Date
              );
            } else {
            }
          }}
        >
          <RangeCalendar
            value={membershipCalendar}
            onChange={setMembershipCalendar}
            amountOfMonths={2}
          />
          <Space h={35} />
          <Center style={{ width: 550 }}>
            <Button variant="light" size="md" type="submit">
              Submit
            </Button>
          </Center>
        </form>
      </Modal>
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
                          student.clubMembership.endDate
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
                          setStudent(student);
                          if (student.clubMembership) {
                            setMembershipCalendar([
                              new Date(student.clubMembership.startDate),
                              new Date(student.clubMembership.endDate),
                            ]);
                          } else {
                            setMembershipCalendar([
                              new Date(),
                              dayjs().add(1, "month").toDate(),
                            ]);
                          }
                          setMembershipModal(true);
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
    </>
  );
};
