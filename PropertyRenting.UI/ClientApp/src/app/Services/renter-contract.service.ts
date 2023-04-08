import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";
import { RenterContract } from "../Models/renter-contract";
import { Pagination } from "../Models/pagination";
import { ContractFinancialTransaction } from "../Models/contract-financial-transaction";

@Injectable({
    providedIn: "root",
})
export class RenterContractService {
    constructor(private httpClient: HttpClient) {}

    GetAll(): Observable<RenterContract[]> {
        return this.httpClient.get<RenterContract[]>(
            environment.ApiURL + "api/v1/RenterContract/list"
        );
    }
    GetById(id: any): Observable<RenterContract> {
        return this.httpClient.get<RenterContract>(
            environment.ApiURL + "api/v1/RenterContract/byId/" + id
        );
    }
    GetByPage(
        pageNumber: number,
        pagesize: number
    ): Observable<Pagination<RenterContract>> {
        return this.httpClient.get<Pagination<RenterContract>>(
            environment.ApiURL +
                "api/v1/RenterContract/list/byPage/" +
                pageNumber +
                "/" +
                pagesize
        );
    }
    Create(contract: RenterContract) {
        return this.httpClient.post(
            environment.ApiURL + "api/v1/RenterContract/create",
            contract
        );
    }
    Edit(id: any, contract: RenterContract) {
        return this.httpClient.put(
            environment.ApiURL + "api/v1/RenterContract/update/" + id,
            contract
        );
    }
    Delete(id: any) {
        return this.httpClient.delete(
            environment.ApiURL + "api/v1/RenterContract/delete/" + id
        );
    }
    Activate(id: any) {
        return this.httpClient.post(
            environment.ApiURL + "api/v1/RenterContract/activate/" + id,
            {}
        );
    }
    Cancel(id: any) {
        return this.httpClient.post(
            environment.ApiURL + "api/v1/RenterContract/cancel/" + id,
            {}
        );
    }
    GetInstallments(id: any): Observable<ContractFinancialTransaction[]> {
        return this.httpClient.get<ContractFinancialTransaction[]>(
            environment.ApiURL +
                "api/v1/RenterContract/byId/" +
                id +
                "/installments"
        );
    }
}
