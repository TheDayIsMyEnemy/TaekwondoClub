import {
  Box,
  Button,
  Center,
  Checkbox,
  Modal,
  Select,
  Space,
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
      gender: "Male",
      phoneNumber: "",
      membershipPeriod: [
        new Date(),
        dayjs().add(1, "month").add(1, "day").toDate(),
      ],
    },
    validate: {
      firstName: (value) => (/^\s+$/.test(value) ? "Invalid First Name" : null),
      lastName: (value) => (/^\s+$/.test(value) ? "Invalid Last Name" : null),
      phoneNumber: (value) => {
        if (!value) {
          return null;
        }
        return /$^(0\d{10})$|^(359\d{10})$/.test(value)
          ? null
          : "Invalid number";
      },
      membershipPeriod: (values) => {
        if (
          isAddMembershipChecked &&
          (values[0] == null || values[1] == null)
        ) {
          return "Invalid dates";
        }
        return null;
      },
    },
  });

  return (
    <Modal
      size="md"
      opened={opened}
      onClose={onClose}
      title="Add a new student"
    >
      <Box sx={{ maxWidth: 300 }} mx="auto">
        <form
          onSubmit={(e) => {
            e.preventDefault();
            const formValidation = form.validate();
            if (formValidation.hasErrors) {
              return;
            }
            if (!isAddMembershipChecked) {
              form.values.membershipPeriod = [null, null];
            }
            onSubmit(form.values);
          }}
        >
          <TextInput
            required
            label="First Name"
            placeholder="Enter your first name"
            onChange={(event) => {
              form.clearFieldError("firstName");
              form.setFieldValue("firstName", event.currentTarget.value);
            }}
            error={form.errors.firstName}
          />
          <TextInput
            required
            label="Last Name"
            placeholder="Enter your last name"
            onChange={(event) => {
              form.clearFieldError("firstName");
              form.setFieldValue("lastName", event.currentTarget.value);
            }}
            error={form.errors.lastName}
          />
          <Select
            value={form.values.gender}
            label="Gender"
            data={[
              {
                value: "Male",
                label: "Male",
              },
              { value: "Female", label: "Female" },
            ]}
            onChange={(value) => form.setFieldValue("gender", value as string)}
          />
          <DatePicker
            placeholder="Pick your birth date"
            label="Birth Date"
            defaultValue={form.values.birthDate}
            onChange={(date) => {
              form.setFieldValue("birthDate", date);
            }}
          />
          <TextInput
            label="Phone Number"
            placeholder="Enter your phone number"
            onChange={(event) => {
              form.clearFieldError("phoneNumber");
              form.setFieldValue("phoneNumber", event.currentTarget.value);
            }}
            error={form.errors.phoneNumber}
          />
          <Checkbox
            checked={isAddMembershipChecked}
            label="Add Membership"
            mt="sm"
            onChange={() => setIsAddMembershipChecked(!isAddMembershipChecked)}
          />
          {isAddMembershipChecked && (
            <DateRangePicker
              required={true}
              label="Membership period"
              placeholder="Pick dates range"
              value={form.values.membershipPeriod}
              onChange={(values) => {
                form.clearFieldError("membershipPeriod");
                form.setFieldValue("membershipPeriod", values);
              }}
              mt="sm"
              error={form.errors.membershipPeriod}
            />
          )}
          <Space h={35} />
          <Center>
            <Button variant="light" size="md" type="submit">
              Submit
            </Button>
          </Center>
        </form>
      </Box>
    </Modal>
  );
};
