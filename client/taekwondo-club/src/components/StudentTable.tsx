import React, { useState, useEffect, FC } from "react";
import { Table } from "@mantine/core";
import { Student } from "../types";

type StudentTableProps = {
  students: Student[];
};

export const StudentTable: FC<StudentTableProps> = ({
  students,
}): JSX.Element => {
  const RenewMembersip = async (studentId: number) => {
    const dateNow = new Date().toJSON();
    let endDate = new Date();
    endDate.setDate(endDate.getDate() + 30);
    const body = {
      studentId: studentId,
      startDate: dateNow,
      endDate: endDate.toJSON(),
    };
    await fetch("https://localhost:7258/clubmembership", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        // 'Accept': 'application/json',
        // "Access-Control-Allow-Origin": "*",
      },
      // mode: "same-origin",
      body: JSON.stringify(body),
    })
      .then((res) => res.json())
      .then((json) => {
        // console.log(json);
        // setStudents(json);
      })
      .catch((error) => console.log(error));
  };

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
                <td>
                  {new Date(student.birthDate).toLocaleDateString("bg-BG")}
                </td>
                <td>
                  {student.clubMembership?.isActive
                    ? `Active until ${new Date(
                        student.clubMembership.endDate
                      ).toLocaleDateString()}`
                    : student.clubMembership?.endDate != null
                    ? `Expired on ${student.clubMembership.endDate}`
                    : `Not created`}
                </td>
                <td></td>
              </tr>
            );
          })}
      </tbody>
    </Table>
  );
};
