import {
  Box,
  Button,
  Center,
  Checkbox,
  Modal,
  Select,
  TextInput,
} from "@mantine/core";
import { DatePicker, DateRangePicker } from "@mantine/dates";
import { useForm } from "@mantine/form";
import dayjs from "dayjs";
import { useState } from "react";
import { CreateStudent } from "../types";

type AddStudentModalProps = {
  opened: boolean;
  onSubmit: (student: CreateStudent) => void;
  onClose: () => void;
};

export const AddStudentModal: React.FC<AddStudentModalProps> = ({
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
      phoneNumber: "",
      membershipPeriod: null,
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
      membershipPeriod: (values) => {
        if (values != null && (values[0] == null || values[1] == null)) {
          return true;
        }
        return null;
      },
    },
  });

  return (
    <Modal
      size="sm"
      opened={opened}
      onClose={onClose}
      title="Add a new student"
    >
      <Box sx={{ maxWidth: 300 }} mx="auto">
        <form
          onSubmit={form.onSubmit((values) => {
            onSubmit(values);
          })}
        >
          <TextInput
            required
            label="First Name"
            {...form.getInputProps("firstName")}
          />
          <TextInput
            required
            label="Last Name"
            {...form.getInputProps("lastName")}
          />
          <Select
            required
            label="Gender"
            data={[
              {
                value: "Male",
                label: "Male",
              },
              { value: "Female", label: "Female" },
            ]}
            {...form.getInputProps("gender")}
          />
          <DatePicker label="Birth Date" {...form.getInputProps("birthDate")} />
          <TextInput
            label="Phone Number"
            {...form.getInputProps("phoneNumber")}
          />
          <Checkbox
            checked={isAddMembershipChecked}
            label="Add Membership"
            mt="sm"
            onChange={() => {
              let membershipPeriod: [Date | null, Date | null] | null = null;
              if (!isAddMembershipChecked) {
                membershipPeriod = [
                  new Date(),
                  dayjs().add(1, "month").toDate(),
                ];
              }
              form.setFieldValue("membershipPeriod", membershipPeriod);
              setIsAddMembershipChecked(!isAddMembershipChecked);
            }}
          />
          {isAddMembershipChecked && (
            <DateRangePicker
              required
              label="Membership period"
              my="sm"
              {...form.getInputProps("membershipPeriod")}
            />
          )}
          <Center mt="xs">
            <Button color="blue" size="md" type="submit">
              Submit
            </Button>
          </Center>
        </form>
      </Box>
    </Modal>
  );
};
