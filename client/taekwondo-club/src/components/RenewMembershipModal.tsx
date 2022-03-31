import { Button, Center, Modal, Space } from "@mantine/core";
import { RangeCalendar } from "@mantine/dates";
import { FC, useState } from "react";
import dayjs from "dayjs";

type RenewMembershipModalProps = {
  opened: boolean;
  onSubmit: (startDate: Date, endDate: Date) => void;
  onClose: () => void;
};

export const RenewMembershipModal: FC<RenewMembershipModalProps> = ({
  opened,
  onSubmit,
  onClose,
}): JSX.Element => {
  const startDate = new Date();
  const endDate = dayjs().add(1, "month").add(1, "day").toDate();
  const [calendar, setCalendar] = useState<[Date | null, Date | null]>([
    startDate,
    endDate,
  ]);

  return (
    <Modal size="xl" opened={opened} onClose={onClose} title="Renew Membership">
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
