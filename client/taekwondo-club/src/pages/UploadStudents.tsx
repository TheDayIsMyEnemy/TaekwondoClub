import { Button, Paper, List, ThemeIcon, Space } from "@mantine/core";
import { useState, useCallback } from "react";
import { IssueClosedIcon, IssueDraftIcon } from "@primer/octicons-react";
// import Sheet from '../components/Sheet'
// import { motion } from 'framer-motion'

// const AnimatedDraftIcon = motion(IssueDraftIcon)
import { FileUpload } from "../components/FileUpload";

export const UploadStudents = () => {
  const [isOpen, setIsOpen] = useState(true);

  const handleToggle = useCallback(() => {
    setIsOpen((open) => !open);
  }, []);

  return (
    <Paper p="xs" shadow="xs" withBorder>
      <form onSubmit={() => console.log("hi")}>
        <FileUpload />
        <Button variant="light" size="xs">Upload</Button>
      </form>
    </Paper>
  );
};
