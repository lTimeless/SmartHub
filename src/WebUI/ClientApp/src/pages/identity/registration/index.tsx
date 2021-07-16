import React from 'react';

// type PasswordStrength = 'Too weak' | 'Could be stronger' | 'Strong password' | '';

const RegistrationPage = () => {
  // const [credentials, setCredentials] = useState({
  //   userName: '',
  //   password: '',
  //   role: Roles.User // default role, can be changed after registration
  // });
  // const [passwordStrengthText, setPasswordStrengthText] = useState<PasswordStrength>('');

  // const checkPasswordStrength = () => {
  //   const strongRegex = new RegExp('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])(?=.{8,})');
  //   const mediumRegex = new RegExp(
  //     '^(((?=.*[a-z])(?=.*[A-Z]))|((?=.*[a-z])(?=.*[0-9]))|((?=.*[A-Z])(?=.*[0-9])))(?=.{6,})'
  //   );
  //   if (strongRegex.test(credentials.password)) {
  //     setPasswordStrengthText('Strong password');
  //   } else if (mediumRegex.test(credentials.password)) {
  //     setPasswordStrengthText('Could be stronger');
  //   } else {
  //     setPasswordStrengthText('Too weak');
  //   }
  // };

  return (
    <div>
      Registration
      {/* TODO on blur and keydown event from input trigger "checkPasswordStrength" */}
      {/* Password strength meter */}
      {/* <div className="flex items-center mt-4 h-3">
      <div className="w-2/3 flex justify-between h-2">
        <div
                : class="{
          'bg-red-400':
        passwordStrengthText === 'Too weak' ||
        passwordStrengthText === 'Could be stronger' ||
        passwordStrengthText === 'Strong password'
                }"
        className="h-2 rounded-full mr-1 w-1/3 bg-gray-300"
              />
        <div
                : class="{
          'bg-yellow-400':
        passwordStrengthText === 'Could be stronger' || passwordStrengthText === 'Strong password'
                }"
        className="h-2 rounded-full mr-1 w-1/3 bg-gray-300"
              />
        <div
                : class="{'bg-green-400': passwordStrengthText === 'Strong password' }"
        class="h-2 rounded-full w-1/3 bg-gray-300"
              />
      </div>
      <div className="text-gray-500 font-medium text-sm ml-3 leading-none">
        {{ passwordStrengthText }}
      </div>
    </div> */}
    </div>
  );
};

export default RegistrationPage;
