import { StudentTable } from "../components/StudentTable";
import { Paper, Pagination, Space, Text, ScrollArea } from "@mantine/core";
import { useEffect, useState } from "react";
import { Student } from "../types";
import {
  createClubMembership,
  updateClubMembership,
  getStudents,
} from "../api/requests";
import { MembershipModal } from "../components/MembershipModal";

export const Students = () => {
  const [activePage, setPage] = useState<number>(1);
  const [students, setStudents] = useState<Student[]>([]);
  const [selectedStudent, setSelectedStudent] = useState<Student | null>(null);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [isMembershipModalOpened, setIsMembershipModalOpened] =
    useState<boolean>(false);

  useEffect(() => {
    if (!isLoading) {
      loadStudents();
    }
  }, []);

  const loadStudents = () => {
    setIsLoading(true);
    getStudents().then((res) => {
      setStudents(res.data);
      setIsLoading(false);
    });
  };

  const onMembershipFormSubmit = (startDate: Date, endDate: Date) => {
    if (selectedStudent?.clubMembership) {
      updateClubMembership(
        selectedStudent.clubMembership.id,
        startDate,
        endDate
      ).then(() => {
        setIsMembershipModalOpened(false);
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
      <MembershipModal
        opened={isMembershipModalOpened}
        onSubmit={onMembershipFormSubmit}
        onClose={() => setIsMembershipModalOpened(false)}
        calendarStartDate={selectedStudent?.clubMembership?.startDate as Date}
        calendarEndDate={selectedStudent?.clubMembership?.endDate as Date}
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
            <Text>Current page: {activePage}</Text>
            <StudentTable
              students={students}
              onMembershipModalOpen={(selectedStudent) => {
                setIsMembershipModalOpened(true);
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
