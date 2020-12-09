import gql from 'graphql-tag';

// Initialize
export const GET_APP_CONFIG = gql`
  query getAppConfig {
    appConfig {
      configFilePath
      applicationName
      configFileName
      configName
      description
      address {
        street
        city
        state
        country
        zipCode
      }
      isActive
      unitSystem
      timeZone
      downloadServerUrl
      saveXLimit
      deleteXAmountAfterLimit
      logFolderPath
      logFolderName
      pluginFolderPath
      pluginFolderName
      configFolderPath
      configFolderName
      baseFolderName
      downloadServerUrl
    }
  }
`;
export const ApplicationIsActive = gql`
  query ApplicationIsActive {
    applicationIsActive
  }
`;
export const UsersExist = gql`
  query UsersExist {
    usersExist
  }
`;

// Identity
export const whoAmI = gql`
  query GetMe {
    me {
      id
      userName
      createdAt
    }
  }
`;

// Device
export const getDevices = gql`
  query GetDevices {
    devices {
      id
      name
      description
      primaryConnection
      secondaryConnection
      createdAt
      lastModifiedAt
      createdBy
      lastModifiedBy
      pluginName
      pluginTypes
      ip {
        ipv4
      }
      company {
        name
        shortName
      }
    }
  }
`;

// Group
export const getGroups = gql`
  query GetGroups {
    groups {
      id
      name
      isSubGroup
      createdBy
      lastModifiedBy
      lastModifiedAt
      createdAt
      description
      devices {
        id
        name
        pluginName
        pluginName
      }
    }
  }
`;
