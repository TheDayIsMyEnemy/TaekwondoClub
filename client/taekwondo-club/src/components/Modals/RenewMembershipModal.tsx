import {
  Button,
  Center,
  Input,
  Modal,
  NumberInput,
  Space,
} from "@mantine/core";
import { RangeCalendar } from "@mantine/dates";
import { FC, useState } from "react";
import dayjs from "dayjs";

type RenewMembershipModalProps = {
  opened: boolean;
  onSubmit: (
    startDate: Date,
    endDate: Date,
    subscriptionFee: number | undefined
  ) => void;
  onClose: () => void;
};

const RenewMembershipModal: FC<RenewMembershipModalProps> = ({
  opened,
  onSubmit,
  onClose,
}): JSX.Element => {
  const startDate = new Date();
  const endDate = dayjs().add(1, "month").toDate();
  const [calendar, setCalendar] = useState<[Date | null, Date | null]>([
    startDate,
    endDate,
  ]);
  const [subscriptionFee, setSubscriptionFee] = useState<number | undefined>(
    20
  );

  return (
    <Modal size="xl" opened={opened} onClose={onClose} title="Renew Membership">
      <form
        onSubmit={(e) => {
          e.preventDefault();
          onSubmit(calendar[0]!, calendar[1]!, subscriptionFee);
        }}
      >
        <NumberInput
          value={subscriptionFee}
          onChange={(val) => setSubscriptionFee(val)}
          label="Subscription Fee"
          sx={{ width: 150 }}
          mb={15}
          required
        />
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

export default RenewMembershipModal;
