import { HttpRequest } from "./HttpRequest";
export class Repository {
    constructor(emitter) {
        if (new.target === Repository){
            throw new TypeError("Abstract repository class can't be instantiated!");
        }
        this.httpRequest = new HttpRequest(emitter);
        this.resource = new.target.name.replace('Repository', '').toLowerCase();
    }
    async getAll() { return await this.httpRequest.send("GET", `/${this.resource}`); }
    async getById(id) { return await this.httpRequest.send("GET", `/${this.resource}/${id}`); }
    async add(form) { return await this.httpRequest.send("POST", `/${this.resource}`, form); }
    async update(id, form) { return await this.httpRequest.send("PUT", `/${this.resource}/${id}`, form); }
    async delete(id) { return await this.httpRequest.send("DELETE", `/${this.resource}/${id}`); }
}