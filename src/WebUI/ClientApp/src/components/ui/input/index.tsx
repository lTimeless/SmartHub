import clsx from 'clsx';
import React from 'react';

interface IInputProps {
  placeholder?: string;
  type: string;
  name: string;
  label: string;
  value: any;
  width?: string;
  onChange?: (e: React.ChangeEvent<HTMLInputElement>) => void;
  onKeyDown?: (e: React.KeyboardEvent<HTMLInputElement>) => void;
  onBlur?: (e: React.ChangeEvent<HTMLInputElement>) => void;
}

const Input = ({ placeholder, type, name, label, width, value, onChange, onKeyDown, onBlur }: IInputProps) => {
  return (
    <div className={clsx(width || 'w-48', 'relative')}>
      <input
        id={name}
        type={type}
        name={name}
        value={value}
        onChange={onChange}
        onKeyDown={onKeyDown}
        onBlur={onBlur}
        placeholder={placeholder}
        className='peer h-10 w-full border-b-2 border-gray-300 text-gray-900 placeholder-transparent focus:outline-none focus:border-primary'
      />
      <label
        htmlFor={name}
        className='absolute left-0 -top-3.5 text-gray-600 text-sm transition-all peer-placeholder-shown:text-base peer-placeholder-shown:text-gray-400 
        peer-placeholder-shown:top-2 peer-focus:-top-3.5 peer-focus:text-gray-600 peer-focus:text-sm'
      >
        {label}
      </label>
    </div>
  );
};

export default Input;
