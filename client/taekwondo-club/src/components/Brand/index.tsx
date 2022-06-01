import { useMantineColorScheme, useMantineTheme } from "@mantine/styles";
import { Group, ActionIcon } from "@mantine/core";
import { Sun, MoonStars } from "tabler-icons-react";

const Brand = (): JSX.Element => {
  const theme = useMantineTheme();
  const { colorScheme, toggleColorScheme } = useMantineColorScheme();
  const dark = colorScheme === "dark";

  return (
    <div
      style={{
        paddingLeft: theme.spacing.xs,
        paddingRight: theme.spacing.xs,
        paddingBottom: theme.spacing.lg,
        borderBottom: `1px solid ${
          dark ? theme.colors.dark[4] : theme.colors.gray[2]
        }`,
      }}
    >
      <Group position="right">
        <ActionIcon
          variant="outline"
          color={dark ? "gray" : "blue"}
          onClick={() => toggleColorScheme()}
          title="Toggle color scheme"
          size={33}
        >
          {dark ? <Sun size={22} /> : <MoonStars size={22} />}
        </ActionIcon>
      </Group>
    </div>
  );
};

export default Brand;
