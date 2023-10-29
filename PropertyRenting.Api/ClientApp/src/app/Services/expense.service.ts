import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";
import { Expense } from "../Models/expense";
import { Pagination } from "../Models/pagination";
import { Lookup } from "../Models/lookup";

@Injectable({
    providedIn: "root",
})
export class ExpenseService {
    constructor(private httpClient: HttpClient) {}

    GetAll(): Observable<Expense[]> {
        return this.httpClient.get<Expense[]>(
            environment.ApiURL + "api/v1/expense/list"
        );
    }
    GetLookup(): Observable<Lookup[]> {
        return this.httpClient.get<Lookup[]>(
            environment.ApiURL + "api/v1/Expense/lookup"
        );
    }
    GetById(id: any): Observable<Expense> {
        return this.httpClient.get<Expense>(
            environment.ApiURL + "api/v1/expense/byId/" + id
        );
    }
    GetByPage(
        pageNumber: number,
        pagesize: number
    ): Observable<Pagination<Expense>> {
        return this.httpClient.get<Pagination<Expense>>(
            environment.ApiURL +
                "api/v1/expense/list/byPage/" +
                pageNumber +
                "/" +
                pagesize
        );
    }
    Create(expense: Expense) {
        return this.httpClient.post(
            environment.ApiURL + "api/v1/expense/create",
            expense
        );
    }
    Edit(id: any, expense: Expense) {
        return this.httpClient.put(
            environment.ApiURL + "api/v1/expense/update/" + id,
            expense
        );
    }
    Delete(id: any) {
        return this.httpClient.delete(
            environment.ApiURL + "api/v1/expense/delete/" + id
        );
    }
}
