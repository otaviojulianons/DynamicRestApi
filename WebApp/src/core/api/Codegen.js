import axios from 'axios';

const codegenGetClientTypes = () => {
    return axios.get("https://generator.swagger.io/api/gen/clients");
}

const codegenGetServerTypes = () => {
    return axios.get("https://generator.swagger.io/api/gen/servers");
}


export {
    codegenGetClientTypes, 
    codegenGetServerTypes,
};