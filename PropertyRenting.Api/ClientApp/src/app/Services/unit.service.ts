import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";
import { Pagination } from "../Models/pagination";
import { Unit } from "../Models/unit";
import { Lookup } from "../Models/lookup";
import { UnitLookup } from "../Models/unit-lookup";
import { UnitCount } from "../Models/unit-count";
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
    GetLookup(): Observable<UnitLookup[]> {
        return this.httpClient.get<UnitLookup[]>(
            environment.ApiURL + "api/v1/Unit/lookup"
        );
    }
    GetAvailable(buildingId: any): Observable<Lookup[]> {
        return this.httpClient.get<Lookup[]>(
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

    GetCountByCity(): Observable<UnitCount[]> {
        return this.httpClient.get<UnitCount[]>(
            environment.ApiURL + "api/v1/unit/count-by-city"
        );
    }
    GetCountByDistrict(): Observable<UnitCount[]> {
        return this.httpClient.get<UnitCount[]>(
            environment.ApiURL + "api/v1/unit/count-by-district"
        );
    }
}
