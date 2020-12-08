import gql from 'graphql-tag';

// Initialize
export const getAppConfig = gql`
  {
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
export const checkApp = gql`
  {
    checkApp
  }
`;
export const checkUsers = gql`
  {
    checkUsers
  }
`;

// Identity
export const whoAmI = gql`
  {
    me {
      id
      userName
      createdAt
    }
  }
`;

// Device
export const getDevices = gql`
  {
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
  {
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
