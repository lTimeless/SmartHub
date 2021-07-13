import React, { ReactNode } from "react";

interface IAuthProps {
  children?: ReactNode;
}

const AuthLayout = (props: IAuthProps) => {
  return (
    <div>
      <h1>Auth Layout</h1>
      {props.children}
    </div>
  );
}

export default AuthLayout;