import { useEffect, useState } from "react";
import {
  Paper,
  Pagination,
  Space,
  Text,
  ScrollArea,
  Button,
} from "@mantine/core";
import { showNotification } from "@mantine/notifications";
import { StudentTable } from "../components/StudentTable";
import { AddStudentModal } from "../components/Modals/AddStudentModal";
import { RenewMembershipModal } from "../components/Modals/RenewMembershipModal";
import { DeleteStudentModal } from "../components/Modals/DeleteStudentModal";
import { CreateStudent, Student } from "../types";
import {
  createMembership,
  updateMembership,
  getStudents,
  createStudent,
  deleteStudent,
} from "../api/requests";
import { useTranslation } from "react-i18next";
import { PersonAddIcon } from "@primer/octicons-react";

export const Students = () => {
  const [activePage, setPage] = useState<number>(1);
  const [students, setStudents] = useState<Student[]>([]);
  const [selectedStudent, setSelectedStudent] = useState<Student | null>(null);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [isMembershipModalOpened, setIsMembershipModalOpened] =
    useState<boolean>(false);
  const [isDeleteStudentModalOpened, setIsDeleteStudentModalOpened] =
    useState<boolean>(false);
  const [isAddStudentModalOpened, setIsAddStudentModalOpened] =
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

  const onAddStudentFormSubmit = (student: CreateStudent) => {
    createStudent(student).then(() => {
      setIsAddStudentModalOpened(false);
      showNotification({
        title: "Add Student",
        message: `${student.firstName} ${student.lastName} has been created successfully!`,
        color: "green",
      });
      loadStudents();
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
    if (selectedStudent?.membership) {
      updateMembership(selectedStudent.membership.id, startDate, endDate).then(
        () => {
          setIsMembershipModalOpened(false);
          showNotification({
            title: "Renew Membership",
            message: "The membership has been updated successfully!",
            color: "green",
          });
          loadStudents();
        }
      );
    } else {
      createMembership(selectedStudent!.id, startDate, endDate).then(() => {
        setIsMembershipModalOpened(false);
        loadStudents();
      });
    }
  };

  const { t } = useTranslation();

  return (
    <>
      <AddStudentModal
        opened={isAddStudentModalOpened}
        onSubmit={onAddStudentFormSubmit}
        onClose={() => setIsAddStudentModalOpened(false)}
      />
      <DeleteStudentModal
        studentFullName={`${selectedStudent?.firstName} ${selectedStudent?.lastName}`}
        opened={isDeleteStudentModalOpened}
        onSubmit={onDeleteStudentFormSubmit}
        onClose={() => setIsDeleteStudentModalOpened(false)}
      />
      <RenewMembershipModal
        opened={isMembershipModalOpened}
        onSubmit={onRenewMembershipFormSubmit}
        onClose={() => setIsMembershipModalOpened(false)}
      />
      <Paper mb="xs" p="xs" shadow="xs" withBorder style={{ height: "70px" }}>
        <Button
          leftIcon={<PersonAddIcon size={16} />}
          size="sm"
          color="blue"
          onClick={() => setIsAddStudentModalOpened(true)}
        >
          {t("Add Student")}
        </Button>
      </Paper>
      <Paper
        p="xs"
        shadow="xs"
        withBorder
        component={ScrollArea}
        style={{ height: "calc(100vh - 110px)" }}
      >
        {isLoading ? (
          <Text>No data found...</Text>
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
