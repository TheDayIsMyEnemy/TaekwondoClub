import { Group, Text, useMantineTheme, MantineTheme } from "@mantine/core";
import { Upload, FileImport, X, Icon as TablerIcon } from "tabler-icons-react";
import { Dropzone, DropzoneStatus, MIME_TYPES } from "@mantine/dropzone";
import { useState } from "react";

function getIconColor(status: DropzoneStatus, theme: MantineTheme) {
  return status.accepted
    ? theme.colors[theme.primaryColor][theme.colorScheme === "dark" ? 4 : 6]
    : status.rejected
    ? theme.colors.red[theme.colorScheme === "dark" ? 4 : 6]
    : theme.colorScheme === "dark"
    ? theme.colors.dark[0]
    : theme.colors.gray[7];
}

const FileUploadIcon = ({
  status,
  ...props
}: React.ComponentProps<TablerIcon> & { status: DropzoneStatus }) => {
  if (status.accepted) {
    return <Upload {...props} />;
  }

  if (status.rejected) {
    return <X {...props} />;
  }

  return <FileImport {...props} />;
};

const DropzoneChildren = (status: DropzoneStatus, theme: MantineTheme) => (
  <Group
    position="center"
    spacing="xl"
    style={{ minHeight: 220, pointerEvents: "none" }}
  >
    <FileUploadIcon
      status={status}
      style={{ color: getIconColor(status, theme) }}
      size={80}
    />

    <div>
      <Text size="xl" inline>
        Drag and drop or click to select file
      </Text>
      <Text size="sm" color="dimmed" inline mt={7}>
        Only CSV files
      </Text>
    </div>
  </Group>
);

export const FileUpload = () => {
  const theme = useMantineTheme();
  const [accepted, setAccepted] = useState<boolean>(false);

  return (
    <Dropzone
      onDrop={(files) => {

      }}
      onReject={(files) => console.log(files)}
      accept={[MIME_TYPES.csv]}
      multiple={false}
    >
      {(status) => DropzoneChildren(status, theme)}
    </Dropzone>
  );
};
