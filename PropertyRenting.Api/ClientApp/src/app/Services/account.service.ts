import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";
import { Account } from "../Models/account";
import { Enum } from "../Models/enum";
import { Pagination } from "../Models/pagination";

@Injectable({
    providedIn: "root",
})
export class AccountService {
    AccountType: Enum[] = [
        { id: 1, description: "AccountType.Assets" },
        { id: 2, description: "AccountType.Liabilities" },
        { id: 3, description: "AccountType.Expenses" },
        { id: 4, description: "AccountType.Revenues" },
        { id: 5, description: "AccountType.Total" },
    ];
    constructor(private httpClient: HttpClient) {}

    GetAll(): Observable<Account[]> {
        return this.httpClient.get<Account[]>(
            environment.ApiURL + "api/v1/Account/list"
        );
    }
    GetAllGrid(): Observable<Account[]> {
        return this.httpClient.get<Account[]>(
            environment.ApiURL + "api/v1/Account/list-grid"
        );
    }
    GetById(id: any): Observable<Account> {
        return this.httpClient.get<Account>(
            environment.ApiURL + "api/v1/Account/byId/" + id
        );
    }
    GetByPage(
        pageNumber: number,
        pagesize: number
    ): Observable<Pagination<Account>> {
        return this.httpClient.get<Pagination<Account>>(
            environment.ApiURL +
                "api/v1/Account/list/byPage/" +
                pageNumber +
                "/" +
                pagesize
        );
    }
    Create(contract: Account) {
        return this.httpClient.post(
            environment.ApiURL + "api/v1/Account/create",
            contract
        );
    }
    Edit(id: any, contract: Account) {
        return this.httpClient.put(
            environment.ApiURL + "api/v1/Account/update/" + id,
            contract
        );
    }
    Delete(id: any) {
        return this.httpClient.delete(
            environment.ApiURL + "api/v1/Account/delete/" + id
        );
    }

    GetAccountTypeById(id: number): string {
        return this.AccountType?.find((x) => x.id == id)?.description || "";
    }
}
