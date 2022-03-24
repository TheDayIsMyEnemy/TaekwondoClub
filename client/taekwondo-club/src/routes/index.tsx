import { Home } from "../pages/Home";
import { Students } from "../pages/Students";
import { UploadStudents } from "../pages/UploadStudents";

import type { Icon } from "@primer/octicons-react";
import { HomeIcon, UploadIcon, TableIcon } from "@primer/octicons-react";

export type Route = {
  name: string;
  path: string;
  element: React.FC | (() => JSX.Element);
  icon: Icon | (() => JSX.Element);
  color: string;
  private?: boolean;
};

const routes: Route[] = [
  {
    name: "Home",
    path: "/",
    element: Home,
    color: "pink",
    icon: HomeIcon,
  },
  {
    name: "Students",
    path: "/students",
    element: Students,
    icon: TableIcon,
    color: "lime",
  },
  {
    name: "Upload Students",
    path: "/upload-students",
    element: UploadStudents,
    icon: UploadIcon,
    color: "lime",
  },
];

export default routes;
