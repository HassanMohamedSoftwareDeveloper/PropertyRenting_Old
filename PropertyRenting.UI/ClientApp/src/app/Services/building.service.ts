import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";
import { Building } from "../Models/building";
import { Enum } from "../Models/enum";
import { Pagination } from "../Models/pagination";

@Injectable({
    providedIn: "root",
})
export class BuildingService {
    BuildingTypes: Enum[] = [
        { id: 1, description: "BuildingType.Building" },
        { id: 2, description: "BuildingType.Mall" },
        { id: 3, description: "BuildingType.Housing" },
        { id: 4, description: "BuildingType.Managerial" },
    ];

    ConstructionStatuses: Enum[] = [
        { id: 1, description: "ConstructionStatus.New" },
        { id: 2, description: "ConstructionStatus.Middle" },
        { id: 3, description: "ConstructionStatus.Old" },
    ];

    constructor(private httpClient: HttpClient) {}

    GetAllBuildings(): Observable<Building[]> {
        return this.httpClient.get<Building[]>(
            environment.ApiURL + "api/v1/building/list"
        );
    }
    GetById(id: any): Observable<Building> {
        return this.httpClient.get<Building>(
            environment.ApiURL + "api/v1/building/byId/" + id
        );
    }
    GetByPage(
        pageNumber: number,
        pagesize: number
    ): Observable<Pagination<Building>> {
        return this.httpClient.get<Pagination<Building>>(
            environment.ApiURL +
                "api/v1/building/list/byPage/" +
                pageNumber +
                "/" +
                pagesize
        );
    }
    CreateNewBuilding(building: Building) {
        return this.httpClient.post(
            environment.ApiURL + "api/v1/building/create",
            building
        );
    }
    EditBuilding(id: any, building: Building) {
        return this.httpClient.put(
            environment.ApiURL + "api/v1/building/update/" + id,
            building
        );
    }
    DeleteBuilding(id: any) {
        return this.httpClient.delete(
            environment.ApiURL + "api/v1/building/delete/" + id
        );
    }
}
