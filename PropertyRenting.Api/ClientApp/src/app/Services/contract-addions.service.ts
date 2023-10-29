import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";
import { ContractAddions } from "../Models/contract-addions";
import { Pagination } from "../Models/pagination";

@Injectable({
    providedIn: "root",
})
export class ContractAddionsService {
    constructor(private httpClient: HttpClient) {}

    GetAll(): Observable<ContractAddions[]> {
        return this.httpClient.get<ContractAddions[]>(
            environment.ApiURL + "api/v1/ContractAdditions/list"
        );
    }
    GetById(id: any): Observable<ContractAddions> {
        return this.httpClient.get<ContractAddions>(
            environment.ApiURL + "api/v1/ContractAdditions/byId/" + id
        );
    }
    GetByPage(
        pageNumber: number,
        pagesize: number
    ): Observable<Pagination<ContractAddions>> {
        return this.httpClient.get<Pagination<ContractAddions>>(
            environment.ApiURL +
                "api/v1/ContractAdditions/list/byPage/" +
                pageNumber +
                "/" +
                pagesize
        );
    }
    Create(ContractAddions: ContractAddions) {
        return this.httpClient.post(
            environment.ApiURL + "api/v1/ContractAdditions/create",
            ContractAddions
        );
    }
    Edit(id: any, ContractAddions: ContractAddions) {
        return this.httpClient.put(
            environment.ApiURL + "api/v1/ContractAdditions/update/" + id,
            ContractAddions
        );
    }
    Delete(id: any) {
        return this.httpClient.delete(
            environment.ApiURL + "api/v1/ContractAdditions/delete/" + id
        );
    }
}
