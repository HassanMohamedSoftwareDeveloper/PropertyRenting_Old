import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";
import { Employee } from "../Models/employee";
import { Pagination } from "../Models/pagination";

@Injectable({
    providedIn: "root",
})
export class EmployeeService {
    constructor(private httpClient: HttpClient) {}

    GetAllEmployees(): Observable<Employee[]> {
        return this.httpClient.get<Employee[]>(
            environment.ApiURL + "api/v1/Employee/list"
        );
    }
    GetByPage(
        pageNumber: number,
        pagesize: number
    ): Observable<Pagination<Employee>> {
        return this.httpClient.get<Pagination<Employee>>(
            environment.ApiURL +
                "api/v1/Employee/list/byPage/" +
                pageNumber +
                "/" +
                pagesize
        );
    }
    CreateNewEmployee(employee: Employee) {
        return this.httpClient.post(
            environment.ApiURL + "api/v1/Employee/create",
            employee
        );
    }
    EditEmployee(id: any, employee: Employee) {
        return this.httpClient.put(
            environment.ApiURL + "api/v1/Employee/update/" + id,
            employee
        );
    }
    DeleteEmployee(id: any) {
        return this.httpClient.delete(
            environment.ApiURL + "api/v1/Employee/delete/" + id
        );
    }
}
