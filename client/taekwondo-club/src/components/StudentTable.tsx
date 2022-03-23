import React, { useState, useEffect } from "react";
import { AppShell, Table } from "@mantine/core";


type Student = {
  id: number;
  firstName: string;
  lastName: string;
  gender: string;
  phoneNumber: string;
  birthDate: Date;
  isActive: boolean;
};

const StudentTable = () => {
  const [students, setStudents] = useState<Array<Student>>();

  useEffect(() => {
    fetch("https://localhost:7258/students")
      .then((res) => res.json())
      .then((json) => {
        // console.log(json);
        setStudents(json);
      })
      .catch((error) => console.log(error));
  }, []);

  return (

    <Table>
      <thead>
        <tr>
          <th>Id</th>
          <th>First Name</th>
          <th>Last Name</th>
          <th>Gender</th>
          <th>PhoneNumber</th>
          <th>Date Of Birth</th>
          <th>Status</th>
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
                <td>{student.phoneNumber}</td>
                <td>
                  {new Date(student.birthDate).toLocaleDateString("bg-BG")}
                </td>
                <td>{student.isActive ? "Yes" : "No"}</td>
              </tr>
            );
          })}
      </tbody>
    </Table>
  );
};

export default StudentTable;
