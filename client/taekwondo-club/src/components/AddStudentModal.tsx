import {
  Box,
  Button,
  Center,
  Modal,
  Select,
  Space,
  TextInput,
} from "@mantine/core";
import { DatePicker } from "@mantine/dates";
import { useForm } from "@mantine/form";
import { CreateStudentRequest } from "../types";

type AddStudentModalProps = {
  opened: boolean;
  onSubmit: (student: CreateStudentRequest) => void;
  onClose: () => void;
};

export const AddStudentModal: React.FC<AddStudentModalProps> = ({
  opened,
  onSubmit,
  onClose,
}): JSX.Element => {
  const initialValues: CreateStudentRequest = {
    firstName: "",
    lastName: "",
    birthDate: null,
    gender: "Male",
    phoneNumber: "",
    // clubMembership: null,
  };
  const form = useForm({
    initialValues: initialValues,
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
            const s = form.validate();
            if (s.hasErrors) {
              return;
            }
            console.log(form.values);
            console.log(s);
            // const req:CreateStudentRequest = {
            //   firstName: form.values.firstName,
            //   lastName: form.values.lastName,
            //   gender: form.values.gender,
            //   birthDate: form.values.birthDate,

            // }
            onSubmit(form.values as CreateStudentRequest);
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
            required={true}
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
