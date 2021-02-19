import { Roles, Routes } from '@/types/enums';

type SidebarListItem = {
  label: string;
  rolesRequired: Roles[];
  route: Routes;
  iconName: string;
  children: never[];
  active: boolean;
};

const sidebarList: SidebarListItem[] = [
  // Guest views
  {
    label: 'Home',
    rolesRequired: [Roles.Guest, Roles.User, Roles.Admin],
    route: Routes.Home,
    iconName: 'Home',
    children: [],
    active: true
  },
  // User views
  {
    label: 'Groups',
    rolesRequired: [Roles.User, Roles.Admin],
    route: Routes.Groups,
    iconName: 'Folder',
    children: [],
    active: true
  },
  {
    label: 'Devices',
    rolesRequired: [Roles.User, Roles.Admin],
    route: Routes.Devices,
    iconName: 'Device',
    children: [],
    active: true
  },
  {
    label: 'Automations',
    rolesRequired: [Roles.User, Roles.Admin],
    route: Routes.Automations,
    iconName: 'Repeat',
    children: [],
    active: true
  },
  {
    label: 'Users',
    rolesRequired: [Roles.User, Roles.Admin],
    route: Routes.Users,
    iconName: 'Users',
    children: [],
    active: false
  },
  {
    label: 'Statistics',
    rolesRequired: [Roles.User, Roles.Admin],
    route: Routes.Statistics,
    iconName: 'Pie-chart',
    children: [],
    active: true
  },
  {
    label: 'Plugins',
    rolesRequired: [Roles.User, Roles.Admin],
    route: Routes.Plugins,
    iconName: 'Plugin',
    children: [],
    active: true
  },
  {
    label: 'Configuration',
    rolesRequired: [Roles.User, Roles.Admin],
    route: Routes.Configuration,
    iconName: 'Settings',
    children: [],
    active: true
  }
  // Admin views
  // TODO werden zu tabs oder mini-sidebar in der config view von der Admin view
  // {
  //   name: 'System',
  //   roleNeeded: [Roles.Admin],
  //   path: Routes.System,
  //   icon: 'mdi-desktop-classic',
  //   children: []
  // },
  // {
  //   name: 'Health',
  //   roleNeeded: [Roles.Admin],
  //   path: Routes.Health,
  //   icon: 'mdi-clipboard-pulse',
  //   children: []
  // }
  // {
  //   name: 'Activity',
  //   roleNeeded: [Roles.Admin],
  //   path: Routes.Activity,
  //   icon: 'mdi-calendar-alert',
  //   children: []
  // },
  // {
  //   name: 'Logs',
  //   roleNeeded: [Roles.Admin],
  //   path: Routes.Logs,
  //   icon: 'mdi-file-document',
  //   children: []
  // }
  // TODO: move to toolbar or help icon on the bottom of sidebar
  // {
  //   name: 'About',
  //   roleNeeded: ['Guest', 'User', 'Admin'],
  //   path: '/about',
  //   children: [{ title: 'About', icon: 'mdi-information', path: '/about' }]
  // }
];

export const useSidebar = () => {
  return {
    sidebarList
  };
};
