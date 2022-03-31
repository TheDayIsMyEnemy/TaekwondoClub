import { useEffect, useState } from "react";
import { Paper, Pagination, Space, Text, ScrollArea } from "@mantine/core";
import { showNotification } from "@mantine/notifications";
import { StudentTable } from "../components/StudentTable";
import { RenewMembershipModal } from "../components/RenewMembershipModal";
import { Student } from "../types";
import {
  createClubMembership,
  updateClubMembership,
  getStudents,
  deleteStudent,
} from "../api/requests";
import { DeleteStudentModal } from "../components/DeleteStudentModal";

export const Students = () => {
  const [activePage, setPage] = useState<number>(1);
  const [students, setStudents] = useState<Student[]>([]);
  const [selectedStudent, setSelectedStudent] = useState<Student | null>(null);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [isMembershipModalOpened, setIsMembershipModalOpened] =
    useState<boolean>(false);
  const [isDeleteStudentModalOpened, setIsDeleteStudentModalOpened] =
    useState<boolean>(false);

  useEffect(() => {
    if (!isLoading) {
      loadStudents();
    }
    return () => {
      setStudents([]);
    };
  }, []);

  const loadStudents = () => {
    setIsLoading(true);
    getStudents().then((res) => {
      setStudents(res.data);
      setIsLoading(false);
    });
  };

  const onDeleteStudentFormSubmit = () => {
    deleteStudent(selectedStudent!.id).then(() => {
      setIsDeleteStudentModalOpened(false);
      showNotification({
        title: "Delete Student",
        message: `${selectedStudent?.firstName} ${selectedStudent?.lastName}
           has been deleted successfully!`,
        color: "green",
      });
      loadStudents();
    });
  };

  const onRenewMembershipFormSubmit = (startDate: Date, endDate: Date) => {
    if (selectedStudent?.clubMembership) {
      updateClubMembership(
        selectedStudent.clubMembership.id,
        startDate,
        endDate
      ).then(() => {
        setIsMembershipModalOpened(false);
        showNotification({
          title: "Renew Membership",
          message: "The membership has been updated successfully!",
          color: "green",
        });
        loadStudents();
      });
    } else {
      createClubMembership(selectedStudent!.id, startDate, endDate).then(() => {
        setIsMembershipModalOpened(false);
        loadStudents();
      });
    }
  };

  return (
    <>
      <RenewMembershipModal
        opened={isMembershipModalOpened}
        onSubmit={onRenewMembershipFormSubmit}
        onClose={() => setIsMembershipModalOpened(false)}
      />
      <DeleteStudentModal
        studentFullName={`${selectedStudent?.firstName} ${selectedStudent?.lastName}`}
        opened={isDeleteStudentModalOpened}
        onSubmit={onDeleteStudentFormSubmit}
        onClose={() => setIsDeleteStudentModalOpened(false)}
      />
      <Paper
        p="xs"
        shadow="xs"
        withBorder
        component={ScrollArea}
        style={{ height: "calc(100vh - 110px)" }}
      >
        {isLoading ? (
          <Text>Loading...</Text>
        ) : (
          <>
            <StudentTable
              students={students}
              onRenewMembershipModalOpen={(selectedStudent) => {
                setIsMembershipModalOpened(true);
                setSelectedStudent(selectedStudent);
              }}
              onDeleteStudentModalOpen={(selectedStudent) => {
                setIsDeleteStudentModalOpened(true);
                setSelectedStudent(selectedStudent);
              }}
            />
            <Space h="sm" />
            <Pagination
              total={20}
              color="orange"
              size="md"
              radius="md"
              siblings={1} // default to 1
              boundaries={2} // default to 1
              page={activePage}
              onChange={setPage}
            />
          </>
        )}
      </Paper>
    </>
  );
};
