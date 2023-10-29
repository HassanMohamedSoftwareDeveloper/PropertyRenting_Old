import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";
import { City } from "../Models/city";
import { Pagination } from "../Models/pagination";
import { Lookup } from "../Models/lookup";

@Injectable({
    providedIn: "root",
})
export class CityService {
    constructor(private httpClient: HttpClient) {}

    GetAllCities(): Observable<City[]> {
        return this.httpClient.get<City[]>(
            environment.ApiURL + "api/v1/city/list"
        );
    }
    GetLookup(countryId: string): Observable<Lookup[]> {
        return this.httpClient.get<Lookup[]>(
            environment.ApiURL + "api/v1/city/lookup/" + countryId
        );
    }
    GetByPage(
        pageNumber: number,
        pagesize: number
    ): Observable<Pagination<City>> {
        return this.httpClient.get<Pagination<City>>(
            environment.ApiURL +
                "api/v1/city/list/byPage/" +
                pageNumber +
                "/" +
                pagesize
        );
    }
    CreateNewCity(city: City) {
        return this.httpClient.post(
            environment.ApiURL + "api/v1/city/create",
            city
        );
    }
    EditCity(id: any, city: City) {
        return this.httpClient.put(
            environment.ApiURL + "api/v1/city/update/" + id,
            city
        );
    }
    DeleteCity(id: any) {
        return this.httpClient.delete(
            environment.ApiURL + "api/v1/city/delete/" + id
        );
    }
}
