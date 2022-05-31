import { Link } from "react-router-dom";
import { createStyles } from "@mantine/styles";
import { ThemeIcon, Group, Text, Anchor } from "@mantine/core";
import routes, { Route } from "../../routes";

const useStyles = createStyles((theme) => ({
  button: {
    display: "block",
    padding: theme.spacing.xs,
    borderRadius: theme.radius.sm,
    color: theme.colorScheme === "dark" ? theme.colors.dark[0] : theme.black,

    "&:hover": {
      textDecoration: "none",
      backgroundColor:
        theme.colorScheme === "dark"
          ? theme.colors.dark[6]
          : theme.colors.gray[0],
    },

    "&:focus": {
      textDecoration: "none",
      backgroundColor:
        theme.colorScheme === "dark"
          ? theme.colors.dark[6]
          : theme.colors.gray[0],
    },
  },
}));

const MainLink = ({ icon: Icon, color, name, path }: Route) => {
  const { classes } = useStyles();

  return (
    <>
      <Anchor component={Link} to={path} className={classes.button}>
        <Group>
          <ThemeIcon color={color} variant="light" size="lg">
            <Icon size="small" />
          </ThemeIcon>

          {/* <Text size="sm">{name}</Text> */}
        </Group>
      </Anchor>
    </>
  );
};

const MainLinks = () => {
  const links = routes.map((link) => <MainLink {...link} key={link.name} />);
  return <div>{links}</div>;
};

export default MainLinks;
