import { StudentTable } from "../components/StudentTable";
import { Paper, Pagination, Space, Text, ScrollArea } from "@mantine/core";
import { useEffect, useState } from "react";
import { Student } from "../types";
import { getStudents, updateClubMembership } from "../actions";
import { MembershipModal } from "../components/MembershipModal";

export const Students = () => {
  const [activePage, setPage] = useState<number>(1);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [students, setStudents] = useState<Student[]>([]);

  const loadStudents = () => {
    setIsLoading(true);
    getStudents().then((res) => {
      setStudents(res.data);
      setIsLoading(false);
    });
  };

  useEffect(() => {
    if (!isLoading) {
      loadStudents();
    }
  }, []);

  const onUpdateClubMembership = (
    clubMembershipId: number,
    startDate: Date,
    endDate: Date
  ) => {
    updateClubMembership(clubMembershipId, startDate, endDate).then(
      loadStudents
    );
  };

  return (
    // <SetMembershipModal/>
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
            onUpdateClubMembership={onUpdateClubMembership}
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
  );
};
