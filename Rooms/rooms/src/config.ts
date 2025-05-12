// src/config.ts
export interface AppConfig {
  AUTHURL: string;
}

let config: AppConfig;

export const loadConfig = async (): Promise<void> => {
  const response = await fetch('/config.json');
  config = await response.json();
};

export const getConfig = (): AppConfig => {
  if (!config) {
    throw new Error('Configuration has not been loaded yet.');
  }
  return config;
};