import Button from '@/components/ui/button';
import Card from '@/components/ui/card';
import Input from '@/components/ui/input';
import { Routes } from '@/types/enums';
import React, { useState } from 'react';
import { Link } from 'react-router-dom';

const LoginPage = () => {
  const [credentials, setCredentials] = useState({
    userName: '',
    password: ''
  });

  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setCredentials({
      ...credentials,
      [event.target.name]: event.target.value
    });
  };

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    console.log(credentials);
    // TODO sent gql request to backend
    event.preventDefault();
  };

  return (
    <Card
      background={
        <div className='relative h-56 bg-sky-500'>
          <svg className='absolute bottom-0' xmlns='http://www.w3.org/2000/svg' viewBox='0 0 1440 320'>
            <path
              fill='#ffffff'
              fillOpacity='1'
              d='M0,64L48,80C96,96,192,128,288,128C384,128,480,96,576,85.3C672,75,768,85,864,122.7C960,160,1056,224,1152,245.3C1248,267,1344,245,1392,234.7L1440,224L1440,320L1392,320C1344,320,1248,320,1152,320C1056,320,960,320,864,320C768,320,672,320,576,320C480,320,384,320,288,320C192,320,96,320,48,320L0,320Z'
            />
          </svg>
        </div>
      }
      children={
        <div className='px-10 pb-8 bg-white rounded-tr-4xl'>
          <h1 className='text-2xl font-semibold text-gray-900'>Welcome back!</h1>
          <form className='mt-12' onSubmit={e => handleSubmit(e)} method='POST'>
            <div className='relative'>
              <Input
                label='Username'
                type='text'
                name='userName'
                placeholder='Username'
                value={credentials.userName}
                width='w-full'
                onChange={e => handleChange(e)}
              />
            </div>
            <div className='mt-10 relative'>
              <Input
                label='Password'
                type='password'
                name='password'
                placeholder='Password'
                value={credentials.password}
                width='w-full'
                onChange={e => handleChange(e)}
              />
            </div>

            <div className='mt-10'>
              <Button type='submit' label='Sign in' primary width='w-full' />
            </div>
          </form>
          <div className='flex justify-between'>
            {/* TODO add forgot PW route */}
            <Link
              to={Routes.Login}
              className='mt-4 block text-sm text-center font-medium text-primary hover:underline focus:outline-none focus:ring-2 focus:ring-primary'
            >
              Forgot your password?
            </Link>

            <Link
              to={Routes.Register}
              className='mt-4 block text-sm text-center font-medium text-primary hover:underline focus:outline-none focus:ring-2 focus:ring-primary'
            >
              Create account
            </Link>
          </div>
        </div>
      }
    />
  );
};

export default LoginPage;
