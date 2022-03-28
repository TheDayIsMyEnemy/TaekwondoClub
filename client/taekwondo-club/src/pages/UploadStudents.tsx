import { Button, Paper } from "@mantine/core";
import { useState } from "react";
import { FileUpload } from "../components/FileUpload";
import axios from "axios";

export const UploadStudents = () => {
  const [file, setFile] = useState<File>();

  const uploadFile = (event: any) => {
    event.preventDefault();
    const formData = new FormData();
    formData.append("students", file as Blob);

    axios
      .post("https://localhost:7258/UploadStudents", formData, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      })
      .then((res) => console.log(res))
      .catch((er) => console.log(er));
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
