import React, { ReactNode } from 'react';

interface IAuthProps {
  children?: ReactNode;
}

const AuthLayout = ({ children }: IAuthProps) => {
  return (
    <div>
      <h1>Auth Layout</h1>
      {children}
    </div>
  );
};

export default AuthLayout;
