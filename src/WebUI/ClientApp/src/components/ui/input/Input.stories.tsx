import React from 'react';

import { Meta } from '@storybook/react';
import Input from '.';

export default {
  title: 'Components/Input',
  component: Input
} as Meta;

export const Text: React.VFC<{}> = () => <Input label='Test' name='text' type='text' placeholder='placeholder' />;
export const Password: React.VFC<{}> = () => <Input label='Test' name='password' type='password' placeholder='placeholder' />;
