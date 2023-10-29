import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";
import { Nationality } from "../Models/nationality";
import { Pagination } from "../Models/pagination";
import { Lookup } from "../Models/lookup";

@Injectable({
    providedIn: "root",
})
export class NationalityService {
    constructor(private httpClient: HttpClient) {}

    GetAll(): Observable<Nationality[]> {
        return this.httpClient.get<Nationality[]>(
            environment.ApiURL + "api/v1/nationality/list"
        );
    }
    GetLookup(): Observable<Lookup[]> {
        return this.httpClient.get<Lookup[]>(
            environment.ApiURL + "api/v1/Nationality/lookup"
        );
    }
    GetById(id: any): Observable<Nationality> {
        return this.httpClient.get<Nationality>(
            environment.ApiURL + "api/v1/nationality/byId/" + id
        );
    }
    GetByPage(
        pageNumber: number,
        pagesize: number
    ): Observable<Pagination<Nationality>> {
        return this.httpClient.get<Pagination<Nationality>>(
            environment.ApiURL +
                "api/v1/nationality/list/byPage/" +
                pageNumber +
                "/" +
                pagesize
        );
    }
    Create(nationality: Nationality) {
        return this.httpClient.post(
            environment.ApiURL + "api/v1/nationality/create",
            nationality
        );
    }
    Edit(id: any, nationality: Nationality) {
        return this.httpClient.put(
            environment.ApiURL + "api/v1/nationality/update/" + id,
            nationality
        );
    }
    Delete(id: any) {
        return this.httpClient.delete(
            environment.ApiURL + "api/v1/nationality/delete/" + id
        );
    }
}
