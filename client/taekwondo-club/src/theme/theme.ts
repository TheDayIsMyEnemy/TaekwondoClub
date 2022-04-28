import { MantineTheme } from "@mantine/core";
import { CSSObject } from "@mantine/styles";

const theme = {};

export const globalStyles = (theme: MantineTheme): CSSObject[] => [
  {
    "*, *::before, *::after": {
      boxSizing: "border-box",
    },

    body: {
      ...theme.fn.fontStyles(),
      backgroundColor:
        theme.colorScheme === "dark" ? theme.colors.dark[7] : theme.white,
      color: theme.colorScheme === "dark" ? theme.colors.dark[0] : theme.black,
      lineHeight: theme.lineHeight,
    },
  },
];

export const styles = {};

export default theme;
