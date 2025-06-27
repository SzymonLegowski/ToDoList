import instance from '../axios';

export class HttpRequest {
    constructor(emitter) {
        this.emitter = emitter;
    }

    async send(httpMethod, url, data = null) {
        try{
            const config = {
            method: httpMethod,
            url,
            };
            if (httpMethod.toUpperCase() === 'GET' && data) {
                config.params = data;
            } else {
                config.data = data;
            }
            const response = await instance.request(config);
            return response.data;
            // const response = await instance.request({
            //     method: httpMethod,
            //     url,
            //     data,
            // });
            // return response.data;
        } catch (err) {
            this.emitter?.emit('alert', {
                msg: err.response.data,
                variant: 'error'
            })
            throw err
        }
    }
}