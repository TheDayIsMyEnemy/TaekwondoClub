import { useEffect, useState } from "react";
import {
  Paper,
  Pagination,
  Space,
  Text,
  ScrollArea,
  ActionIcon,
} from "@mantine/core";
import { showNotification } from "@mantine/notifications";
import { StudentTable } from "../components/StudentTable";
import {
  AddStudentModal,
  DeleteStudentModal,
  RenewMembershipModal,
} from "../components/Modals";
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
import { useMediaQuery } from "@mantine/hooks";

export const Students = () => {
  const [activePage, setActivePage] = useState<number>(1);
  const [students, setStudents] = useState<Student[]>([]);
  const [selectedStudents, setSelectedStudents] = useState<Student[]>([]);
  const [selectedStudent, setSelectedStudent] = useState<Student | null>(null);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [isMembershipModalOpened, setIsMembershipModalOpened] =
    useState<boolean>(false);
  const [isDeleteStudentModalOpened, setIsDeleteStudentModalOpened] =
    useState<boolean>(false);
  const [isAddStudentModalOpened, setIsAddStudentModalOpened] =
    useState<boolean>(false);
  const [totalPages, setTotalPages] = useState<number>(0);
  const totalStudentsPerPage = 10;

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
      setTotalPages(Math.ceil(res.data.length / totalStudentsPerPage));
      setSelectedStudentsByPage(res.data, activePage);
      setIsLoading(false);
    });
  };

  const onAddStudentFormSubmit = (student: CreateStudent) => {
    createStudent(student).then((response) => {
      if (student.membership) {
        createMembership(
          response.data,
          student.membership.startDate,
          student.membership.endDate,
          student.membership.subscriptionFee
        ).then(() => onStudentCreated(student.firstName, student.lastName));
      } else {
        onStudentCreated(student.firstName, student.lastName);
      }
    });
  };

  const onStudentCreated = (firstName: string, lastName: string) => {
    setIsAddStudentModalOpened(false);
    showNotification({
      title: "Add Student",
      message: `${firstName} ${lastName} has been created successfully!`,
      color: "green",
    });
    loadStudents();
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
      const newStudents = students.filter((s) => s.id !== selectedStudent!.id);
      setStudents(newStudents);
      setSelectedStudentsByPage(newStudents, activePage);
    });
  };

  const onRenewMembershipFormSubmit = (
    startDate: Date,
    endDate: Date,
    subscriptionFee: number | undefined
  ) => {
    if (selectedStudent?.membership) {
      updateMembership(
        selectedStudent.membership.id,
        startDate,
        endDate,
        subscriptionFee
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
      createMembership(
        selectedStudent!.id,
        startDate,
        endDate,
        subscriptionFee
      ).then(() => {
        setIsMembershipModalOpened(false);
        loadStudents();
      });
    }
  };

  const onPageChanged = (page: number) => {
    setActivePage(page);
    setSelectedStudentsByPage(students, page);
  };

  const setSelectedStudentsByPage = (students: Student[], page: number) => {
    const start = (page - 1) * totalStudentsPerPage;
    const end = start + totalStudentsPerPage;
    const studentsByPage = students.slice(start, end);
    setSelectedStudents(studentsByPage);
  };

  const matchesBreakpoint = useMediaQuery("(min-width: 900px)");
  const height = matchesBreakpoint ? 500 : 360;

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
      <Paper p="xs" shadow="xs" withBorder style={{ height: "70px" }}>
        <ActionIcon
          variant="filled"
          color="primary"
          size={35}
          radius="sm"
          onClick={() => setIsAddStudentModalOpened(true)}
        >
          <PersonAddIcon size={18} />
        </ActionIcon>
      </Paper>
      <Paper
        p="xs"
        shadow="xs"
        withBorder
        component={ScrollArea}
        style={{ height: height }}
      >
        {isLoading ? (
          <Text>Loading...</Text>
        ) : (
          <>
            <StudentTable
              students={selectedStudents}
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
              total={totalPages}
              color="orange"
              size="md"
              radius="md"
              siblings={1}
              boundaries={2}
              page={activePage}
              onChange={onPageChanged}
            />
          </>
        )}
      </Paper>
    </>
  );
};
