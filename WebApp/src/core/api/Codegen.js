import axios from 'axios';
const SWAGGER_URL = "https://api.myjson.com/bins/1843v0";

const codegenGetClientTypes = () => {
    return axios.get("https://generator.swagger.io/api/gen/clients");
}

const codegenGetServerTypes = () => {
    return axios.get("https://generator.swagger.io/api/gen/servers");
}

const codegenPost = ( codeGenType, type ) => {
    console.log(`https://generator.swagger.io/api/gen/${codeGenType}/${type}`);
    return axios.post(
        `https://generator.swagger.io/api/gen/${codeGenType}/${type}`,
        { swaggerUrl: SWAGGER_URL}
    );
}


export {
    codegenGetClientTypes, 
    codegenGetServerTypes,
    codegenPost
};