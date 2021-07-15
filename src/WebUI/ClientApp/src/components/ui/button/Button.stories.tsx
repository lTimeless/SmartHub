import React from 'react';

import { ComponentMeta, ComponentStory } from '@storybook/react';
import Button from '.';

export default {
  title: 'Components/Button',
  component: Button,
  argTypes: {
    onClick: { action: 'clicked' }
  }
} as ComponentMeta<typeof Button>;

const Template: ComponentStory<typeof Button> = args => <Button {...args} />;

export const Primary = Template.bind({});
Primary.args = {
  primary: true,
  label: 'Button',
  type: 'button'
};

export const Secondary = Template.bind({});
Secondary.args = {
  primary: false,
  secondary: true,
  label: 'Button',
  type: 'button'
};
