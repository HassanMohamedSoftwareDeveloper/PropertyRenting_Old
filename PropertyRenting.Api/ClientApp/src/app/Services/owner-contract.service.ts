import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";
import { ContractFinancialTransaction } from "../Models/contract-financial-transaction";
import { OwnerContract } from "../Models/owner-contract";
import { Pagination } from "../Models/pagination";
import { InstallmentsPerDate } from "../Models/installments-per-date";

@Injectable({
    providedIn: "root",
})
export class OwnerContractService {
    constructor(private httpClient: HttpClient) {}

    GetAll(): Observable<OwnerContract[]> {
        return this.httpClient.get<OwnerContract[]>(
            environment.ApiURL + "api/v1/OwnerContract/list"
        );
    }
    GetById(id: any): Observable<OwnerContract> {
        return this.httpClient.get<OwnerContract>(
            environment.ApiURL + "api/v1/OwnerContract/byId/" + id
        );
    }
    GetByPage(
        pageNumber: number,
        pagesize: number
    ): Observable<Pagination<OwnerContract>> {
        return this.httpClient.get<Pagination<OwnerContract>>(
            environment.ApiURL +
                "api/v1/OwnerContract/list/byPage/" +
                pageNumber +
                "/" +
                pagesize
        );
    }
    Create(contract: OwnerContract) {
        return this.httpClient.post(
            environment.ApiURL + "api/v1/OwnerContract/create",
            contract
        );
    }
    Edit(id: any, contract: OwnerContract) {
        return this.httpClient.put(
            environment.ApiURL + "api/v1/OwnerContract/update/" + id,
            contract
        );
    }
    Delete(id: any) {
        return this.httpClient.delete(
            environment.ApiURL + "api/v1/OwnerContract/delete/" + id
        );
    }
    Activate(id: any) {
        return this.httpClient.post(
            environment.ApiURL + "api/v1/OwnerContract/activate/" + id,
            {}
        );
    }
    Cancel(id: any) {
        return this.httpClient.post(
            environment.ApiURL + "api/v1/OwnerContract/cancel/" + id,
            {}
        );
    }
    GetInstallments(id: any): Observable<ContractFinancialTransaction[]> {
        return this.httpClient.get<ContractFinancialTransaction[]>(
            environment.ApiURL +
                "api/v1/OwnerContract/byId/" +
                id +
                "/installments"
        );
    }
    GetInstallmentsPerDate(): Observable<InstallmentsPerDate[]> {
        return this.httpClient.get<InstallmentsPerDate[]>(
            environment.ApiURL + "api/v1/OwnerContract/installments-per-date"
        );
    }
}
