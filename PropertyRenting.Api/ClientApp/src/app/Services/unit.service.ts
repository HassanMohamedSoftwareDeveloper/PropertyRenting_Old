import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";
import { Pagination } from "../Models/pagination";
import { Unit } from "../Models/unit";
@Injectable({
    providedIn: "root",
})
export class UnitService {
    constructor(private httpClient: HttpClient) {}

    GetAll(): Observable<Unit[]> {
        return this.httpClient.get<Unit[]>(
            environment.ApiURL + "api/v1/unit/list"
        );
    }
    GetAvailable(buildingId: any): Observable<Unit[]> {
        return this.httpClient.get<Unit[]>(
            environment.ApiURL + "api/v1/unit/list/available/" + buildingId
        );
    }
    GetById(id: any): Observable<Unit> {
        return this.httpClient.get<Unit>(
            environment.ApiURL + "api/v1/unit/byId/" + id
        );
    }
    GetByPage(
        pageNumber: number,
        pagesize: number
    ): Observable<Pagination<Unit>> {
        return this.httpClient.get<Pagination<Unit>>(
            environment.ApiURL +
                "api/v1/unit/list/byPage/" +
                pageNumber +
                "/" +
                pagesize
        );
    }
    Create(unit: Unit) {
        return this.httpClient.post(
            environment.ApiURL + "api/v1/unit/create",
            unit
        );
    }
    Edit(id: any, unit: Unit) {
        return this.httpClient.put(
            environment.ApiURL + "api/v1/unit/update/" + id,
            unit
        );
    }
    Delete(id: any) {
        return this.httpClient.delete(
            environment.ApiURL + "api/v1/unit/delete/" + id
        );
    }
}
