const config = {
    development: {
        content : {
            uri: 'http://localhost:5300'
        }
    },
    production: {
        content : {
            uri: 'http://localhost:5200'
        }
    }
};

export default function Config() {
    return config[process.env.NODE_ENV];
}