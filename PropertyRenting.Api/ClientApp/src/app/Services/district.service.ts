import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";
import { District } from "../Models/district";
import { Pagination } from "../Models/pagination";
import { Lookup } from "../Models/lookup";

@Injectable({
    providedIn: "root",
})
export class DistrictService {
    constructor(private httpClient: HttpClient) {}

    GetAllDistricts(): Observable<District[]> {
        return this.httpClient.get<District[]>(
            environment.ApiURL + "api/v1/District/list"
        );
    }
    GetLookup(cityId: string): Observable<Lookup[]> {
        return this.httpClient.get<Lookup[]>(
            environment.ApiURL + "api/v1/District/lookup/" + cityId
        );
    }
    GetByPage(
        pageNumber: number,
        pagesize: number
    ): Observable<Pagination<District>> {
        return this.httpClient.get<Pagination<District>>(
            environment.ApiURL +
                "api/v1/District/list/byPage/" +
                pageNumber +
                "/" +
                pagesize
        );
    }
    CreateNewDistrict(district: District) {
        return this.httpClient.post(
            environment.ApiURL + "api/v1/District/create",
            district
        );
    }
    EditDistrict(id: any, district: District) {
        return this.httpClient.put(
            environment.ApiURL + "api/v1/District/update/" + id,
            district
        );
    }
    DeleteDistrict(id: any) {
        return this.httpClient.delete(
            environment.ApiURL + "api/v1/District/delete/" + id
        );
    }
}
