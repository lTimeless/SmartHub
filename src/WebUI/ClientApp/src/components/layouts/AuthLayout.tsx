import React, { ReactNode } from 'react';

interface IAuthProps {
  children?: ReactNode;
}

const AuthLayout = ({ children }: IAuthProps) => {
  return (
    <div className='flex flex-row justify-center items-center min-h-screen p-6 bg-sky-100'>
      <div className='w-full h-screen xl:w-1/3 flex flex-col justify-center'>{children}</div>
    </div>
  );
};

export default AuthLayout;
