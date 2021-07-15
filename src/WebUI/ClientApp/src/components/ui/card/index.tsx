import clsx from 'clsx';
import React, { ReactNode } from 'react';

interface ICardProps {
  background?: ReactNode;
  children?: ReactNode;
  heigth?: string;
}

const Card = ({ background, children, heigth }: ICardProps) => {
  return (
    <div className={clsx(heigth || '', 'bg-white rounded-xl shadow-md overflow-hidden')}>
      <div className='h-full flex flex-col relative'>
        {background && <div>{background}</div>}
        {children && <div className='p-6'>{children}</div>}
      </div>
    </div>
  );
};

export default Card;
