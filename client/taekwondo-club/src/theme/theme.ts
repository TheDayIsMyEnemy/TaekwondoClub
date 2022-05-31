import { MantineTheme, MantineThemeOverride, Tuple } from "@mantine/core";
import { CSSObject } from "@mantine/styles";

const darkThemeColors: Record<string, Tuple<string, 10>> = {
  // dark: ["#7e6fff", "#1e1e2d", "#1a1a27", "#151521", "#92929f", "#1a1a27", "#1a1a27"],
  // primary: "#7e6fff",
  // surface: "#1e1e2d",
  // background: "#1a1a27",
  // BackgroundGrey = "#151521",
  // AppbarText = "#92929f",
  // AppbarBackground = "rgba(26,26,39,0.8)",
  // DrawerBackground = "#1a1a27",
  // ActionDefault = "#74718e",
  // ActionDisabled = "#9999994d",
  // ActionDisabledBackground = "#605f6d4d",
  // TextPrimary = "#b2b0bf",
  // TextSecondary = "#92929f",
  // TextDisabled = "#ffffff33",
  // DrawerIcon = "#92929f",
  // DrawerText = "#92929f",
  // GrayLight = "#2a2833",
  // GrayLighter = "#1e1e2d",
  // Info = "#4a86ff",
  // Success = "#3dcb6c",
  // Warning = "#ffb545",
  // Error = "#ff3f5f",
  // LinesDefault = "#33323e",
  // TableLines = "#33323e",
  // Divider = "#292838",
  // OverlayLight = "#1e1e2d80"
};

const colors = {
  dark: [],
};

const theme: MantineThemeOverride = {
  colorScheme: "dark",
  fontFamily: "Montserrat",
  colors: darkThemeColors,
  // primaryColor: darkThemeColors.dark[0],
};

export const globalStyles = (theme: MantineTheme): CSSObject[] => [
  {
    "*, *::before, *::after": {
      boxSizing: "border-box",
    },
    body: {
      ...theme.fn.fontStyles(),
      backgroundColor:
        theme.colorScheme === "dark" ? theme.colors.background : theme.white,
      color: theme.colorScheme === "dark" ? theme.colors.dark[0] : theme.black,
      lineHeight: theme.lineHeight,
    },
  },
];

export const styles = {};

export default theme;
