// import type { Icon } from '@primer/octicons-react'
import StudentsTable from '../components/StudentTable'
import studentsIcon from '../assets/icons/students.png';

export type Route = {
  name: string
  path: string
  element: React.FC | (() => JSX.Element)
//   icon: Icon | (() => JSX.Element)
  color: string
  private?: boolean
}

const routes: Route[] = [
  { name: 'Home', path: '/', element: StudentsTable, color: 'pink' },
  {
    name: 'Students',
    path: '/students',
    element: StudentsTable,
    // icon: BellIcon,
    color: 'lime',
  }
]

export default routes
