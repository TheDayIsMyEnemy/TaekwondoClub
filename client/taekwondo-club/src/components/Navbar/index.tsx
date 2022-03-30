import { Navbar, ScrollArea } from "@mantine/core";
import MainLinks from "./MainLinks";

const NavbarSimple = () => {
  return (
    <Navbar width={{ base: 220 }} height="calc(100vh - 65px)">
      <Navbar.Section
        grow
        component={ScrollArea}
        ml={-5}
        mr={-5}
        sx={{ paddingLeft: 5, paddingRight: 5 }}
      >
        <MainLinks />
      </Navbar.Section>
      <Navbar.Section>{/* <User /> */}</Navbar.Section>
    </Navbar>
  );
};

export default NavbarSimple;
