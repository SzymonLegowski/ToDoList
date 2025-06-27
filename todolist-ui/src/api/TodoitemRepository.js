import { Repository } from "./Repository";

export class TodoitemRepository extends Repository{
    
    async getByDate(date) { return await this.httpRequest.send("GET", `/api/${this.resource}`, { date }); }
}