import axios from 'axios';

let apiKey = "";
const AxiosConfiguration = {
    requestInterceptor: null,
    setupInterceptors: () => {
        this.requestInterceptor = axios.interceptors.request.use(
        config => {
            const dyraAPI = config.url.includes("localhost") || config.url.includes("dyra");
            if ( apiKey != null && dyraAPI) {
              config.headers.apiKey = apiKey;
            }
            return config;
        }, 
        error => Promise.reject(error));
    },
    setAPIKey: value => apiKey = value,
    containsAPIKey: () => apiKey !== "",
    clearAPIKey: () => apiKey = "",
    cleanupInterceptors: () => {
        axios.interceptors.request.eject(this.requestInterceptor);
    },
    
};

export default AxiosConfiguration;

