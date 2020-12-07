import gql from 'graphql-tag';

// App
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

// Init
export const checkApp = gql`
  {
    checkApp {
      data
      success
      message
    }
  }
`;

export const checkUsers = gql`
  {
    checkUsers {
      data
      success
      message
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
