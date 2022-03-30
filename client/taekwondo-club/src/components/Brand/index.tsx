import { useMantineColorScheme, useMantineTheme } from "@mantine/styles";
import { Group, ActionIcon } from "@mantine/core";
import { Sun, MoonStars } from "tabler-icons-react";
import { BookmarkFillIcon, BookmarkIcon } from "@primer/octicons-react";

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
      <Group position="apart">
        <ActionIcon variant="light" size={30} color="orange">
          {dark ? <BookmarkIcon /> : <BookmarkFillIcon />}
        </ActionIcon>
        <ActionIcon
          variant="outline"
          color={dark ? "gray" : "blue"}
          onClick={() => toggleColorScheme()}
          title="Toggle color scheme"
        >
          {dark ? <Sun size={18} /> : <MoonStars size={18} />}
        </ActionIcon>
      </Group>
    </div>
  );
};

export default Brand;
