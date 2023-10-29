import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";
import { Enum } from "../Models/enum";
import { Pagination } from "../Models/pagination";
import { Renter } from "../Models/renter";
import { Lookup } from "../Models/lookup";

@Injectable({
    providedIn: "root",
})
export class RenterService {
    RenterTypes: Enum[] = [
        { id: 1, description: "RenterType.Personal" },
        { id: 2, description: "RenterType.Company" },
    ];

    IdentityTypes: Enum[] = [
        { id: 1, description: "IdentityType.NationalNumber" },
        { id: 2, description: "IdentityType.Passport" },
        { id: 3, description: "IdentityType.DrivingLicence" },
        { id: 4, description: "IdentityType.ResidencePermit" },
    ];

    constructor(private httpClient: HttpClient) {}

    GetAll(): Observable<Renter[]> {
        return this.httpClient.get<Renter[]>(
            environment.ApiURL + "api/v1/renter/list"
        );
    }
    GetLookup(): Observable<Lookup[]> {
        return this.httpClient.get<Lookup[]>(
            environment.ApiURL + "api/v1/Renter/lookup"
        );
    }
    GetById(id: any): Observable<Renter> {
        return this.httpClient.get<Renter>(
            environment.ApiURL + "api/v1/renter/byId/" + id
        );
    }
    GetByPage(
        pageNumber: number,
        pagesize: number
    ): Observable<Pagination<Renter>> {
        return this.httpClient.get<Pagination<Renter>>(
            environment.ApiURL +
                "api/v1/renter/list/byPage/" +
                pageNumber +
                "/" +
                pagesize
        );
    }
    Create(renter: Renter) {
        return this.httpClient.post(
            environment.ApiURL + "api/v1/renter/create",
            renter
        );
    }
    Edit(id: any, renter: Renter) {
        return this.httpClient.put(
            environment.ApiURL + "api/v1/renter/update/" + id,
            renter
        );
    }
    Delete(id: any) {
        return this.httpClient.delete(
            environment.ApiURL + "api/v1/renter/delete/" + id
        );
    }
}
