import Navbar from 'src/components/ui/Navbar';
import React, { ReactNode } from 'react';

import Sidebar from '../ui/Sidebar';

interface LayoutProps {
  children?: ReactNode;
}

const Layout = ({ children }: LayoutProps) => {
  return (
    <div className='bg-white h-screen flex flex-row'>
      <Sidebar />
      <div className='flex flex-col w-screen'>
        <Navbar />
        <div className='p-4'>{children}</div>
      </div>
    </div>
  );
};

export default Layout;
