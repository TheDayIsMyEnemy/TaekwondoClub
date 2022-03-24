import { StudentTable } from "../components/StudentTable";
import { Paper, Pagination, Space, Text, ScrollArea } from "@mantine/core";
import { useEffect, useState } from "react";
import { Student } from "../types";
import axios from "axios";

export const Students = () => {
  const [activePage, setPage] = useState<number>(1);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [students, setStudents] = useState<Student[]>([]);

  useEffect(() => {
    if (!isLoading) {
      setIsLoading(true);
      axios.get("https://localhost:7258/students").then((res) => {
        setStudents(res.data);
        setIsLoading(false);
      });
    }
  }, []);

  return (
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
          <StudentTable students={students} />
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
