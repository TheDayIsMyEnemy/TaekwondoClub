import {
  Box,
  Button,
  Center,
  Collapse,
  CSSObject,
  Group,
  Modal,
  NumberInput,
  Radio,
  RadioGroup,
  Switch,
  TextInput,
} from "@mantine/core";
import { DatePicker, DateRangePicker } from "@mantine/dates";
import { useForm } from "@mantine/form";
import dayjs from "dayjs";
import { useState } from "react";
import { CreateStudent, CreateMembership } from "../../types";

type AddStudentModalProps = {
  opened: boolean;
  onSubmit: (student: CreateStudent) => void;
  onClose: () => void;
};

const AddStudentModal: React.FC<AddStudentModalProps> = ({
  opened,
  onSubmit,
  onClose,
}): JSX.Element => {
  const [isAddMembershipChecked, setIsAddMembershipChecked] =
    useState<boolean>(false);
  const form = useForm<CreateStudent>({
    initialValues: {
      firstName: "",
      lastName: "",
      birthDate: null,
      gender: "",
      phoneNumber: null,
      membership: null,
    },
    validate: {
      firstName: (value) => (/^\s*$/.test(value) ? true : null),
      lastName: (value) => (/^\s*$/.test(value) ? true : null),
      gender: (value) => {
        if (value !== "Male" && value !== "Female") {
          return true;
        }
        return null;
      },
      phoneNumber: (value) => {
        if (!value) {
          return null;
        }
        return /^0\d{9}$|^359\d{9}$/.test(value) ? null : "Invalid number";
      },
      membership: (value) => {
        if (
          value != null &&
          (value.startDate == null || value.endDate == null)
        ) {
          return true;
        }
        if (
          value != null &&
          (!value.subscriptionFee || value.subscriptionFee < 20)
        ) {
          return true;
        }
        return null;
      },
    },
  });

  const inputStyles: CSSObject = {
    width: 142,
  };

  return (
    <Modal
      size="sm"
      opened={opened}
      onClose={onClose}
      title="Add student"
      radius="md"
    >
      <Box sx={{ maxWidth: 300 }} mx="auto">
        <form
          onSubmit={form.onSubmit((values) => {
            onSubmit(values);
          })}
        >
          <Group spacing="xs" mb={8}>
            <TextInput
              required
              label="First name"
              sx={inputStyles}
              {...form.getInputProps("firstName")}
            />
            <TextInput
              required
              label="Last name"
              sx={inputStyles}
              {...form.getInputProps("lastName")}
            />
          </Group>
          <Group spacing="xs" mb={8}>
            <DatePicker
              label="Birthdate"
              sx={inputStyles}
              // dropdownType="modal"
              {...form.getInputProps("birthDate")}
            />
            <TextInput
              label="Mobile number"
              sx={inputStyles}
              {...form.getInputProps("phoneNumber")}
            />
          </Group>
          <RadioGroup
            required
            label="Gender"
            spacing="xs"
            size="sm"
            sx={{ "& label": { marginLeft: 10 } }}
            {...form.getInputProps("gender")}
          >
            <Radio value="Male" label="Male" />
            <Radio value="Female" label="Female" />
          </RadioGroup>
          <Switch
            checked={isAddMembershipChecked}
            label="Activate Membership"
            mt="sm"
            mb="xs"
            radius="md"
            onChange={() => {
              let membership = null;
              if (!isAddMembershipChecked) {
                membership = {
                  startDate: new Date(),
                  endDate: dayjs().add(1, "month").toDate(),
                  subscriptionFee: 20,
                };
              }
              form.setFieldValue("membership", membership);
              setIsAddMembershipChecked(!isAddMembershipChecked);
            }}
            styles={{ label: { paddingLeft: 10 } }}
          />
          <Collapse in={isAddMembershipChecked}>
            <NumberInput
              value={
                form.values.membership
                  ? form.values.membership.subscriptionFee
                  : 0
              }
              onChange={(value) => {
                form.values.membership!.subscriptionFee = value;
              }}
              label="Subscription Fee"
              sx={{ width: 150 }}
              mb={10}
            />
            <DateRangePicker
              label="Membership period"
              sx={{ width: 250 }}
              value={[
                form.values.membership
                  ? form.values.membership.startDate
                  : null,
                form.values.membership ? form.values.membership.endDate : null,
              ]}
              onChange={(value) => {
                form.values.membership!.startDate = value[0];
                form.values.membership!.endDate = value[1];
              }}
            />
          </Collapse>
          <Center mt="sm">
            <Button color="green" size="md" type="submit" radius="md">
              Submit
            </Button>
          </Center>
        </form>
      </Box>
    </Modal>
  );
};

export default AddStudentModal;
