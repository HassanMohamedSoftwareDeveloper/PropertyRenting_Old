import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";
import { CashBank } from "../Models/cash-bank";
import { Enum } from "../Models/enum";
import { Pagination } from "../Models/pagination";

@Injectable({
    providedIn: "root",
})
export class CashBankService {
    Types: Enum[] = [
        { id: 1, description: "CashBank.Cash" },
        { id: 2, description: "CashBank.Bank" },
    ];
    constructor(private httpClient: HttpClient) {}

    GetAll(): Observable<CashBank[]> {
        return this.httpClient.get<CashBank[]>(
            environment.ApiURL + "api/v1/CashBank/list"
        );
    }
    GetById(id: any): Observable<CashBank> {
        return this.httpClient.get<CashBank>(
            environment.ApiURL + "api/v1/CashBank/byId/" + id
        );
    }
    GetByPage(
        pageNumber: number,
        pagesize: number
    ): Observable<Pagination<CashBank>> {
        return this.httpClient.get<Pagination<CashBank>>(
            environment.ApiURL +
                "api/v1/CashBank/list/byPage/" +
                pageNumber +
                "/" +
                pagesize
        );
    }
    Create(cash: CashBank) {
        return this.httpClient.post(
            environment.ApiURL + "api/v1/CashBank/create",
            cash
        );
    }
    Edit(id: any, cash: CashBank) {
        return this.httpClient.put(
            environment.ApiURL + "api/v1/CashBank/update/" + id,
            cash
        );
    }
    Delete(id: any) {
        return this.httpClient.delete(
            environment.ApiURL + "api/v1/CashBank/delete/" + id
        );
    }

    GetType(id: number): string {
        return this.Types?.find((x) => x.id == id)?.description || "";
    }
}
