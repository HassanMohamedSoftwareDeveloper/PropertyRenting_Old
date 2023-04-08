import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";
import { Contributer } from "../Models/contributer";
import { Pagination } from "../Models/pagination";

@Injectable({
    providedIn: "root",
})
export class ContributerService {
    constructor(private httpClient: HttpClient) {}

    GetAll(): Observable<Contributer[]> {
        return this.httpClient.get<Contributer[]>(
            environment.ApiURL + "api/v1/Contributer/list"
        );
    }
    GetByPage(
        pageNumber: number,
        pagesize: number
    ): Observable<Pagination<Contributer>> {
        return this.httpClient.get<Pagination<Contributer>>(
            environment.ApiURL +
                "api/v1/Contributer/list/byPage/" +
                pageNumber +
                "/" +
                pagesize
        );
    }
    Create(contributer: Contributer) {
        return this.httpClient.post(
            environment.ApiURL + "api/v1/Contributer/create",
            contributer
        );
    }
    Edit(id: any, contributer: Contributer) {
        return this.httpClient.put(
            environment.ApiURL + "api/v1/Contributer/update/" + id,
            contributer
        );
    }
    Delete(id: any) {
        return this.httpClient.delete(
            environment.ApiURL + "api/v1/Contributer/delete/" + id
        );
    }
}
