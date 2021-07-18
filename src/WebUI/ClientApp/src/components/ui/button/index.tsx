import React from 'react';
import clsx from 'clsx';

interface IButtonProps {
  type: 'submit' | 'reset' | 'button' | undefined;
  label: string;
  width?: string;
  heigth?: string;
  primary?: boolean;
  secondary?: boolean;
  disabled?: boolean;
  onCLick?: () => void;
}
// TODO anstatt primary und secondary, vlt die color aus einem enum/record übergeben und dann die damit verbunden styles laden

const Button = ({ type, label, width, heigth, primary, secondary, disabled, onCLick }: IButtonProps) => {
  return (
    <button
      type={type}
      disabled={disabled}
      onClick={onCLick}
      className={clsx(
        width || 'w-24',
        heigth || 'h-10',
        disabled && 'cursor-not-allowed bg-gray-400',
        primary && !disabled && 'bg-primary',
        primary && !disabled && 'hover:bg-primaryHover',
        primary && !disabled && 'focus:ring-primary',
        secondary && !disabled && 'bg-secondary',
        secondary && !disabled && 'hover:bg-secondaryHover',
        secondary && !disabled && 'focus:ring-secondary',
        'px-4 py-2 rounded text-white font-semibold text-center focus:outline-none focus:ring-2 focus:ring-offset-1 focus:ring-opacity-75'
      )}
    >
      {label}
    </button>
  );
};

export default Button;
