import React from 'react';

import { Meta } from '@storybook/react';
import Card from '.';

export default {
  title: 'Components/Card',
  component: Card
} as Meta;

export const SimpleChildren: React.VFC<{}> = () => (
  <Card>
    <>
      <div className='uppercase tracking-wide text-sm text-indigo-500 font-semibold'>Case study</div>
      <p className='block mt-1 text-lg leading-tight font-medium text-black hover:underline'>
        Finding customers for your new business
      </p>
      <p className='mt-2 text-gray-500'>
        Getting a new business off the ground is a lot of hard work. Here are five ideas you can use to find your first customers.
      </p>
    </>
  </Card>
);

export const WaveyChildren: React.VFC<{}> = () => (
  <Card
    background={
      <div className='relative h-56 bg-rose-500'>
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
      <div className='-mt-12'>
        <div className='uppercase tracking-wide text-sm text-indigo-500 font-semibold'>Case study</div>
        <p className='block mt-1 text-lg leading-tight font-medium text-blacke'>Finding customers for your new business</p>
        <p className='mt-2 text-gray-500'>
          Getting a new business off the ground is a lot of hard work. Here are five ideas you can use to find your first
          customers.
        </p>
      </div>
    }
  />
);
