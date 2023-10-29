import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";
import { Country } from "../Models/country";
import { Pagination } from "../Models/pagination";

@Injectable({
    providedIn: "root",
})
export class CountryService {
    constructor(private httpClient: HttpClient) {}

    GetAllCountries(): Observable<Country[]> {
        return this.httpClient.get<Country[]>(
            environment.ApiURL + "api/v1/country/list"
        );
    }
    GetByPage(
        pageNumber: number,
        pagesize: number
    ): Observable<Pagination<Country>> {
        return this.httpClient.get<Pagination<Country>>(
            environment.ApiURL +
                "api/v1/country/list/byPage/" +
                pageNumber +
                "/" +
                pagesize
        );
    }
    CreateNewCountry(country: Country) {
        return this.httpClient.post(
            environment.ApiURL + "api/v1/country/create",
            country
        );
    }
    EditCountry(id: any, country: Country) {
        return this.httpClient.put(
            environment.ApiURL + "api/v1/country/update/" + id,
            country
        );
    }
    DeleteCountry(id: any) {
        return this.httpClient.delete(
            environment.ApiURL + "api/v1/country/delete/" + id
        );
    }
}
