import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";
import { AccountSetup } from "../Models/account-setup";
import { Pagination } from "../Models/pagination";

@Injectable({
    providedIn: "root",
})
export class AccountSetupSetupService {
    constructor(private httpClient: HttpClient) {}
    Get(): Observable<AccountSetup> {
        return this.httpClient.get<AccountSetup>(
            environment.ApiURL + "api/v1/AccountSetup/"
        );
    }
    Create(contract: AccountSetup) {
        return this.httpClient.post(
            environment.ApiURL + "api/v1/AccountSetup/create",
            contract
        );
    }
    Edit(id: any, contract: AccountSetup) {
        return this.httpClient.put(
            environment.ApiURL + "api/v1/AccountSetup/update/" + id,
            contract
        );
    }
}
