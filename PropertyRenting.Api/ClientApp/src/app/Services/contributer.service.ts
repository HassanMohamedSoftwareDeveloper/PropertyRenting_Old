import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";
import { Contributer } from "../Models/contributer";
import { Pagination } from "../Models/pagination";
import { Lookup } from "../Models/lookup";

@Injectable({
    providedIn: "root",
})
export class ContributerService {
    constructor(private httpClient: HttpClient) {}

    GetAll(): Observable<Contributer[]> {
        return this.httpClient.get<Contributer[]>(
            environment.ApiURL + "api/v1/Contributor/list"
        );
    }
    GetLookup(): Observable<Lookup[]> {
        return this.httpClient.get<Lookup[]>(
            environment.ApiURL + "api/v1/Contributor/lookup"
        );
    }
    GetByPage(
        pageNumber: number,
        pagesize: number
    ): Observable<Pagination<Contributer>> {
        return this.httpClient.get<Pagination<Contributer>>(
            environment.ApiURL +
                "api/v1/Contributor/list/byPage/" +
                pageNumber +
                "/" +
                pagesize
        );
    }
    Create(contributer: Contributer) {
        return this.httpClient.post(
            environment.ApiURL + "api/v1/Contributor/create",
            contributer
        );
    }
    Edit(id: any, contributer: Contributer) {
        return this.httpClient.put(
            environment.ApiURL + "api/v1/Contributor/update/" + id,
            contributer
        );
    }
    Delete(id: any) {
        return this.httpClient.delete(
            environment.ApiURL + "api/v1/Contributor/delete/" + id
        );
    }
}
