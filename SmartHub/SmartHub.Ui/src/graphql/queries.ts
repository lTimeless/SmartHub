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
export const HOME_AND_USERS_EXIST = gql`
  query HomeAndUsersExist {
    applicationIsActive
    usersExist
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
export const WHO_AM_I = gql`
  query GetMe {
    me {
      id
      userName
      createdAt
      createdBy
      lastModifiedAt
      lastModifiedBy
      phoneNumber
      personInfo
      email
      personName {
        firstName
        lastName
        middleName
      }
    }
  }
`;

// Device
export const GET_DEVICES = gql`
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

export const GET_DEVICES_AMOUNT = gql`
  query GetDevices {
    devices {
      id
    }
  }
`;

export const GET_DEVICE_BY_ID = gql`
  query GET_DEVICE_BY_ID($id: String!) {
    devices(where: { id: { eq: $id } }) {
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
export const GET_GROUPS = gql`
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
export const GET_GROUPS_AMOUNT = gql`
  query GetGroups {
    groups {
      id
    }
  }
`;

export const GET_GROUP_BY_ID = gql`
  query GET_GROUP_BY_ID($id: String!) {
    groups(where: { id: { eq: $id } }) {
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
