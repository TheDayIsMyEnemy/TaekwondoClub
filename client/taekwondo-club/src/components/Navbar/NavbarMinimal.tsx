import { Navbar, Center, createStyles, Group } from "@mantine/core";
import MainLinks from "./MainLinks";
// import { MantineLogoSmall } from "../../shared/MantineLogo";

const NavbarMinimal = () => {
  return (
    <Navbar height={750} width={{ base: 80 }}>
      <Center>{/* <MantineLogoSmall /> */}</Center>
      <Navbar.Section grow mt={50}>
        <Group direction="column" align="center" spacing={0}>
          <MainLinks />
        </Group>
      </Navbar.Section>
      <Navbar.Section>
        <Group direction="column" align="center" spacing={0}>
          {/* <NavbarLink icon={SwitchHorizontal} label="Change account" />
          <NavbarLink icon={Logout} label="Logout" /> */}
        </Group>
      </Navbar.Section>
    </Navbar>
  );
};

const useStyles = createStyles((theme) => ({
  link: {
    width: 50,
    height: 50,
    borderRadius: theme.radius.md,
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
    color:
      theme.colorScheme === "dark"
        ? theme.colors.dark[0]
        : theme.colors.gray[7],

    "&:hover": {
      backgroundColor:
        theme.colorScheme === "dark"
          ? theme.colors.dark[5]
          : theme.colors.gray[0],
    },
  },

  active: {
    "&, &:hover": {
      backgroundColor:
        theme.colorScheme === "dark"
          ? theme.fn.rgba(theme.colors[theme.primaryColor][9], 0.25)
          : theme.colors[theme.primaryColor][0],
      color:
        theme.colors[theme.primaryColor][theme.colorScheme === "dark" ? 4 : 7],
    },
  },
}));

export default NavbarMinimal;
