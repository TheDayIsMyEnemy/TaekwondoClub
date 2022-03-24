import React, { FC, useState } from "react";
import { Table, Button, ActionIcon, Group, Modal } from "@mantine/core";
import { RangeCalendar } from "@mantine/dates";
import { Student } from "../types";
import { PencilIcon, DiffRemovedIcon } from "@primer/octicons-react";
import dayjs from 'dayjs';

type StudentTableProps = {
  students: Student[];
};

export const StudentTable: FC<StudentTableProps> = ({
  students,
}): JSX.Element => {
  const [membershipModal, setMembershipModal] = useState(false);
  const [membershipCalendar, setMembershipCalendar] = useState<
    [Date | null, Date | null]
  >([new Date(), dayjs().add(1, "month").toDate()]);
  // const RenewMembersip = async (studentId: number) => {
  //   const dateNow = new Date().toJSON();
  //   let endDate = new Date();
  //   endDate.setDate(endDate.getDate() + 30);
  //   const body = {
  //     studentId: studentId,
  //     startDate: dateNow,
  //     endDate: endDate.toJSON(),
  //   };
  //   await fetch("https://localhost:7258/clubmembership", {
  //     method: "POST",
  //     headers: {
  //       "Content-Type": "application/json",
  //       // 'Accept': 'application/json',
  //       // "Access-Control-Allow-Origin": "*",
  //     },
  //     // mode: "same-origin",
  //     body: JSON.stringify(body),
  //   })
  //     .then((res) => res.json())
  //     .then((json) => {
  //       // console.log(json);
  //       // setStudents(json);
  //     })
  //     .catch((error) => console.log(error));
  // };

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
        <RangeCalendar
          value={membershipCalendar}
          onChange={setMembershipCalendar}
          amountOfMonths={2}
        />
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
                        onClick={() => setMembershipModal(true)}
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
