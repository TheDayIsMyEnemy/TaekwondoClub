import { Button, Center, Modal, Space } from "@mantine/core";
import { RangeCalendar } from "@mantine/dates";
import { useState } from "react";

export const MembershipModal = (
  opened: boolean,
  onSubmit: () => void,
  onClose: () => void
) => {
  const [membershipCalendar, setMembershipCalendar] = useState<
    [Date | null, Date | null]
  >([null, null]);

  return (
    <Modal
      size="xl"
      opened={opened}
      onClose={() => onClose}
      title="Set Membership"
    >
      <form
        onSubmit={(e) => {
          e.preventDefault();
          onSubmit();
        }}
      >
        <RangeCalendar
          value={membershipCalendar}
          onChange={setMembershipCalendar}
          amountOfMonths={2}
        />
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
