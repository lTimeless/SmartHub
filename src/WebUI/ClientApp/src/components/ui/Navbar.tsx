import React from 'react';
import { useLocation } from 'react-router-dom';

const Navbar = () => {
  const route = useLocation();
  const breadcrumb = route.pathname
    .split('/')
    .filter(x => x !== '')
    .join('> ');
  return (
    <div className='w-full border-b-2'>
      <span>{breadcrumb}</span>
    </div>
  );
};

export default Navbar;
