type StringState = {
  capitalize: (value: string) => string;
};

export const useString = (): StringState => {
  const capitalize = (value: string): string => value.charAt(0).toUpperCase() + value.slice(1).toLowerCase();
  return {
    capitalize
  };
};
