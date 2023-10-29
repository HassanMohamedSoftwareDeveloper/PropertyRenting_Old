import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";
import { Enum } from "../Models/enum";

@Injectable({
    providedIn: "root",
})
export class CommonService {
    constructor(private httpClient: HttpClient) {}

    GetBuildingTypes(): Observable<Enum[]> {
        return this.httpClient.get<Enum[]>(
            environment.ApiURL + "api/v1/Common/building-types"
        );
    }
    GetConstructionStatuses(): Observable<Enum[]> {
        return this.httpClient.get<Enum[]>(
            environment.ApiURL + "api/v1/Common/construction-statuses"
        );
    }
}
