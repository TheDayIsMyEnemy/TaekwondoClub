import { Button, Center, Modal, Space } from "@mantine/core";
import { RangeCalendar } from "@mantine/dates";
import { FC, useState } from "react";
import dayjs from "dayjs";

type MembershipModalProps = {
  opened: boolean;
  onSubmit: (startDate: Date, endDate: Date) => void;
  onClose: () => void;
  calendarStartDate: Date | null;
  calendarEndDate: Date | null;
};

export const MembershipModal: FC<MembershipModalProps> = ({
  opened,
  onSubmit,
  onClose,
  calendarStartDate,
  calendarEndDate,
}): JSX.Element => {
  const startDate = calendarStartDate ?? new Date();
  const endDate = calendarEndDate ?? dayjs().add(1, "month").toDate()
  const [calendar, setCalendar] = useState<
    [Date | null, Date | null]
  >([startDate, endDate]);

  return (
    <Modal size="xl" opened={opened} onClose={onClose} title="Set Membership">
      <form
        onSubmit={(e) => {
          e.preventDefault();
          onSubmit(calendar[0]!, calendar[1]!);
        }}
      >
        <RangeCalendar
          value={calendar}
          onChange={setCalendar}
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
