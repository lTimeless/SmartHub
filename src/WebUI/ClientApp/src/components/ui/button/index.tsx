import React from 'react';
import clsx from 'clsx';

interface IButtonProps {
  type: 'submit' | 'reset' | 'button' | undefined;
  label: string;
  width?: string;
  heigth?: string;
  primary?: boolean;
  secondary?: boolean;
  onCLick?: () => void;
}
// TODO anstatt primary und secondary, vlt die color aus einem enum/record übergeben und dann die damit verbunden styles laden

const Button = ({ type, label, width, heigth, primary, secondary, onCLick }: IButtonProps) => {
  return (
    <button
      type={type}
      onClick={onCLick}
      className={clsx(
        width || 'w-24',
        heigth || 'h-10',
        primary && 'bg-primary',
        primary && 'hover:bg-primaryHover',
        primary && 'focus:ring-primary',
        secondary && 'bg-secondary',
        secondary && 'hover:bg-secondaryHover',
        secondary && 'focus:ring-secondary',
        'px-4 py-2 rounded text-white font-semibold text-center focus:outline-none focus:ring-2 focus:ring-offset-1 focus:ring-opacity-75'
      )}
    >
      {label}
    </button>
  );
};

export default Button;
