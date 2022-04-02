import { useState } from "react";
import { Button, Paper } from "@mantine/core";
import { showNotification } from "@mantine/notifications";
import { FileUpload } from "../components/FileUpload";
import client from "../api";

export const UploadStudents = () => {
  const [file, setFile] = useState<File>();

  const uploadFile = (event: any) => {
    event.preventDefault();
    const formData = new FormData();
    formData.append("students", file as Blob);

    if (file) {
      client
        .post("/UploadStudents", formData, {
          headers: {
            "Content-Type": "multipart/form-data",
          },
        })
        .then((res) => {
          showNotification({
            title: "File uploaded successfully",
            message: `${res.data} new students have been added.`,
            color: "green"
          });
        })
        .catch((err) => {
          showNotification({
            title: "There is an error occurred",
            message: `${err}`,
            color: "red"
          });
        });
    }
  };

  return (
    <Paper p="xs" shadow="xs" withBorder>
      <form onSubmit={(e) => uploadFile(e)}>
        <FileUpload setFile={setFile} />
        <Button variant="light" size="md" type="submit">
          Submit
        </Button>
      </form>
    </Paper>
  );
};
