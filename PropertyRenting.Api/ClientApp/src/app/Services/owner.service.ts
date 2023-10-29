import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";
import { Owner } from "../Models/owner";
import { Pagination } from "../Models/pagination";
import { Lookup } from "../Models/lookup";

@Injectable({
    providedIn: "root",
})
export class OwnerService {
    constructor(private httpClient: HttpClient) {}

    GetAllOwners(): Observable<Owner[]> {
        return this.httpClient.get<Owner[]>(
            environment.ApiURL + "api/v1/Owner/list"
        );
    }
    GetLookup(): Observable<Lookup[]> {
        return this.httpClient.get<Lookup[]>(
            environment.ApiURL + "api/v1/Owner/lookup"
        );
    }
    GetByPage(
        pageNumber: number,
        pagesize: number
    ): Observable<Pagination<Owner>> {
        return this.httpClient.get<Pagination<Owner>>(
            environment.ApiURL +
                "api/v1/Owner/list/byPage/" +
                pageNumber +
                "/" +
                pagesize
        );
    }
    CreateNewOwner(owner: Owner) {
        return this.httpClient.post(
            environment.ApiURL + "api/v1/Owner/create",
            owner
        );
    }
    EditOwner(id: any, owner: Owner) {
        return this.httpClient.put(
            environment.ApiURL + "api/v1/Owner/update/" + id,
            owner
        );
    }
    DeleteOwner(id: any) {
        return this.httpClient.delete(
            environment.ApiURL + "api/v1/Owner/delete/" + id
        );
    }
}
