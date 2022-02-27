import React, { useState, useEffect } from "react";
import "../css/StudentTable.css";
import "../css/CheckBox.css";
import CheckBox from "./CheckBox";

type Student = {
  id: number;
  firstName: string;
  lastName: string;
  gender: string;
  phoneNumber: string;
  birthDate: Date;
  isPaid: boolean;
}

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
    <table className="students-table">
      <thead>
        <tr>
          <th>Id</th>
          <th>First Name</th>
          <th>Last Name</th>
          <th>Gender</th>
          <th>PhoneNumber</th>
          <th>Date Of Birth</th>
          <th>Payment</th>
          <th>Check</th>
          
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
                <td>{new Date(student.birthDate).toLocaleDateString("bg-BG")}</td>
                <td>{student.isPaid}</td>
                <td><CheckBox/></td>
              </tr>
            );
          })}
      </tbody>
    </table>
  );
};

export default StudentTable;
