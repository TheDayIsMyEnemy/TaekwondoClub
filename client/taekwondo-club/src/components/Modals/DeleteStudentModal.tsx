import {
  Button,
  Center,
  Group,
  Modal,
  Paper,
  Space,
  Text,
} from "@mantine/core";

type DeleteStudentModalProps = {
  studentFullName: string | undefined;
  opened: boolean;
  onSubmit: () => void;
  onClose: () => void;
};

const DeleteStudentModal: React.FC<DeleteStudentModalProps> = ({
  studentFullName,
  opened,
  onSubmit,
  onClose,
}): JSX.Element => {
  return (
    <Modal size="sm" opened={opened} onClose={onClose} withCloseButton={false}>
      <form
        onSubmit={(e) => {
          e.preventDefault();
          onSubmit();
        }}
      >
        <Paper>
          <Text align="center">Are you sure you want to delete</Text>
          <Text align="center" weight={700}>
            {studentFullName}?
          </Text>
          <Space h={20} />
          <Center>
            <Group>
              <Button color="red" size="md" type="submit">
                Yes
              </Button>
              <Button color="green" size="md" onClick={onClose}>
                No
              </Button>
            </Group>
          </Center>
        </Paper>
      </form>
    </Modal>
  );
};

export default DeleteStudentModal;
