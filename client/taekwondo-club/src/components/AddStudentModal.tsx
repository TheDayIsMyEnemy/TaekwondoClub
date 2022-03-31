import { Button, Center, Modal, Space } from "@mantine/core";

type AddStudentModalProps = {
  opened: boolean;
  onSubmit: () => void;
  onClose: () => void;
};

export const AddStudentModal: React.FC<AddStudentModalProps> = ({
  opened,
  onSubmit,
  onClose,
}): JSX.Element => {
  return (
    <Modal size="xl" opened={opened} onClose={onClose} title="Add a new student">
      <form
        onSubmit={(e) => {
          e.preventDefault();
          onSubmit();
        }}
      >
        <Space h={35} />
        <Center style={{ width: 550 }}>
          <Button variant="light" size="md" type="submit">
            Submit
          </Button>
        </Center>
      </form>
    </Modal>
  );
};
