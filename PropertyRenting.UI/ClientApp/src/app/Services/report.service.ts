import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";
import { AccountBalance } from "../Models/Reports/account-balance";
import { AccountTransaction } from "../Models/Reports/account-transaction";
import { ActiveRenter } from "../Models/Reports/active-renter";
import { BuildingBalance } from "../Models/Reports/building-balance";
import { BuildingRevenuExpense } from "../Models/Reports/building-revenu-expense";
import { BuildingTransaction } from "../Models/Reports/building-transaction";
import { CashBankBalance } from "../Models/Reports/cash-bank-balance";
import { CashBankTransaction } from "../Models/Reports/cash-bank-transaction";
import { OwnerBalance } from "../Models/Reports/owner-balance";
import { OwnerDueInstallments } from "../Models/Reports/owner-due-installments";
import { OwnerTransaction } from "../Models/Reports/owner-transaction";
import { RenterBalance } from "../Models/Reports/renter-balance";
import { RenterDueInstallments } from "../Models/Reports/renter-due-installments";
import { RenterTransaction } from "../Models/Reports/renter-transaction";
import { UnitAvailable } from "../Models/Reports/unit-available";
import { UnitBalance } from "../Models/Reports/unit-balance";
import { UnitCurrent } from "../Models/Reports/unit-current";
import { UnitTransaction } from "../Models/Reports/unit-transaction";

@Injectable({
    providedIn: "root",
})
export class ReportService {
    constructor(private httpClient: HttpClient) {}

    GetActiveRenters(): Observable<ActiveRenter[]> {
        return this.httpClient.get<ActiveRenter[]>(
            environment.ApiURL + "api/v1/reports/renters/active"
        );
    }

    GetBuildingRevenueExpenses(
        buildingId?: any,
        toDate?: Date
    ): Observable<BuildingRevenuExpense[]> {
        return this.httpClient.post<BuildingRevenuExpense[]>(
            environment.ApiURL +
                "api/v1/Reports/building/" +
                buildingId +
                "/revenues-expenses",
            { buildingId: buildingId, toDate: toDate }
        );
    }

    GetRenterDueInstallments(
        toDate?: Date,
        renterId?: any,
        unitId?: any
    ): Observable<RenterDueInstallments[]> {
        return this.httpClient.post<RenterDueInstallments[]>(
            environment.ApiURL + "api/v1/Reports/renter/due-installments/",
            { toDate: toDate, renterId: renterId, unitId: unitId }
        );
    }

    GetOwnerDueInstallments(
        toDate?: Date,
        ownerId?: any,
        buildingId?: any
    ): Observable<OwnerDueInstallments[]> {
        return this.httpClient.post<OwnerDueInstallments[]>(
            environment.ApiURL + "api/v1/Reports/owner/due-installments/",
            { toDate: toDate, ownerId: ownerId, buildingId: buildingId }
        );
    }

    GetAccountBalance(
        accountId?: any,
        fromDate?: Date,
        toDate?: Date
    ): Observable<AccountBalance[]> {
        return this.httpClient.post<AccountBalance[]>(
            environment.ApiURL + "api/v1/Reports/account/balance/",
            { accountId: accountId, fromDate: fromDate, toDate: toDate }
        );
    }

    GetAccountTransactions(
        accountId: any,
        fromDate?: Date,
        toDate?: Date
    ): Observable<AccountTransaction[]> {
        return this.httpClient.post<AccountTransaction[]>(
            environment.ApiURL + "api/v1/Reports/account/transaction/",
            { accountId: accountId, fromDate: fromDate, toDate: toDate }
        );
    }

    GetRenterBalance(
        renterId?: any,
        fromDate?: Date,
        toDate?: Date
    ): Observable<RenterBalance[]> {
        return this.httpClient.post<RenterBalance[]>(
            environment.ApiURL + "api/v1/Reports/renter/balance/",
            { renterId: renterId, fromDate: fromDate, toDate: toDate }
        );
    }

    GetRenterTransactions(
        renterId: any,
        fromDate?: Date,
        toDate?: Date
    ): Observable<RenterTransaction[]> {
        return this.httpClient.post<RenterTransaction[]>(
            environment.ApiURL + "api/v1/Reports/renter/transaction/",
            { renterId: renterId, fromDate: fromDate, toDate: toDate }
        );
    }

    GetOwnerBalance(
        ownerId?: any,
        fromDate?: Date,
        toDate?: Date
    ): Observable<OwnerBalance[]> {
        return this.httpClient.post<OwnerBalance[]>(
            environment.ApiURL + "api/v1/Reports/owner/balance/",
            { ownerId: ownerId, fromDate: fromDate, toDate: toDate }
        );
    }

    GetOwnerTransactions(
        ownerId: any,
        fromDate?: Date,
        toDate?: Date
    ): Observable<OwnerTransaction[]> {
        return this.httpClient.post<OwnerTransaction[]>(
            environment.ApiURL + "api/v1/Reports/owner/transaction/",
            { ownerId: ownerId, fromDate: fromDate, toDate: toDate }
        );
    }

    GetBuildingBalance(
        buildingId?: any,
        fromDate?: Date,
        toDate?: Date
    ): Observable<BuildingBalance[]> {
        return this.httpClient.post<BuildingBalance[]>(
            environment.ApiURL + "api/v1/Reports/building/balance/",
            { buildingId: buildingId, fromDate: fromDate, toDate: toDate }
        );
    }
    GetBuildingTransactions(
        buildingId: any,
        fromDate?: Date,
        toDate?: Date
    ): Observable<BuildingTransaction[]> {
        return this.httpClient.post<BuildingTransaction[]>(
            environment.ApiURL + "api/v1/Reports/building/transaction/",
            { buildingId: buildingId, fromDate: fromDate, toDate: toDate }
        );
    }

    GetUnitBalance(
        unitId?: any,
        fromDate?: Date,
        toDate?: Date
    ): Observable<UnitBalance[]> {
        return this.httpClient.post<UnitBalance[]>(
            environment.ApiURL + "api/v1/Reports/unit/balance/",
            { unitId: unitId, fromDate: fromDate, toDate: toDate }
        );
    }
    GetUnitTransactions(
        unitId: any,
        fromDate?: Date,
        toDate?: Date
    ): Observable<UnitTransaction[]> {
        return this.httpClient.post<UnitTransaction[]>(
            environment.ApiURL + "api/v1/Reports/unit/transaction/",
            { unitId: unitId, fromDate: fromDate, toDate: toDate }
        );
    }

    GetCashBankBalance(
        cashBankId?: any,
        fromDate?: Date,
        toDate?: Date
    ): Observable<CashBankBalance[]> {
        return this.httpClient.post<CashBankBalance[]>(
            environment.ApiURL + "api/v1/Reports/cash-bank/balance/",
            { cashBankId: cashBankId, fromDate: fromDate, toDate: toDate }
        );
    }
    GetCashBankTransactions(
        cashBankId: any,
        fromDate?: Date,
        toDate?: Date
    ): Observable<CashBankTransaction[]> {
        return this.httpClient.post<CashBankTransaction[]>(
            environment.ApiURL + "api/v1/Reports/cash-bank/transaction/",
            { cashBankId: cashBankId, fromDate: fromDate, toDate: toDate }
        );
    }

    GetCurrentUnits(): Observable<UnitCurrent[]> {
        return this.httpClient.get<UnitCurrent[]>(
            environment.ApiURL + "api/v1/Reports/unit/current/"
        );
    }
    GetAvailableUnits(): Observable<UnitAvailable[]> {
        return this.httpClient.get<UnitAvailable[]>(
            environment.ApiURL + "api/v1/Reports/unit/available/"
        );
    }

    ExportActiveRenters(type: string) {
        return this.httpClient.get(
            environment.ApiURL + "api/v1/reports/renters/active/export/" + type,
            {
                responseType: "blob",
            }
        );
    }

    ExportBuildingRevenueExpenses(
        type: string,
        buildingId?: any,
        toDate?: Date
    ) {
        return this.httpClient.post(
            environment.ApiURL +
                "api/v1/Reports/building/" +
                buildingId +
                "/revenues-expenses/export/" +
                type,
            { buildingId: buildingId, toDate: toDate },
            {
                responseType: "blob",
            }
        );
    }

    ExportRenterDueInstallments(
        type: string,
        toDate?: Date,
        renterId?: any,
        unitId?: any
    ) {
        return this.httpClient.post(
            environment.ApiURL +
                "api/v1/Reports/renter/due-installments/export/" +
                type,
            { toDate: toDate, renterId: renterId, unitId: unitId },
            {
                responseType: "blob",
            }
        );
    }

    ExportOwnerDueInstallments(
        type: string,
        toDate?: Date,
        ownerId?: any,
        buildingId?: any
    ) {
        return this.httpClient.post(
            environment.ApiURL +
                "api/v1/Reports/owner/due-installments/export/" +
                type,
            { toDate: toDate, ownerId: ownerId, buildingId: buildingId },
            {
                responseType: "blob",
            }
        );
    }

    ExportAccountBalance(
        type: string,
        accountId?: any,
        fromDate?: Date,
        toDate?: Date
    ) {
        return this.httpClient.post(
            environment.ApiURL +
                "api/v1/Reports/account/balance/export/" +
                type,
            { accountId: accountId, fromDate: fromDate, toDate: toDate },
            {
                responseType: "blob",
            }
        );
    }

    ExportAccountTransactions(
        type: string,
        accountId: any,
        accountName: string,
        fromDate?: Date,
        toDate?: Date
    ) {
        return this.httpClient.post(
            environment.ApiURL +
                "api/v1/Reports/account/transaction/export/" +
                type,
            {
                accountId: accountId,
                fromDate: fromDate,
                toDate: toDate,
                accountName: accountName,
            },
            {
                responseType: "blob",
            }
        );
    }

    ExportRenterBalance(
        type: string,
        renterId?: any,
        fromDate?: Date,
        toDate?: Date
    ) {
        return this.httpClient.post(
            environment.ApiURL + "api/v1/Reports/renter/balance/export/" + type,
            { renterId: renterId, fromDate: fromDate, toDate: toDate },
            {
                responseType: "blob",
            }
        );
    }

    ExportRenterTransactions(
        type: string,
        renterId: any,
        renterName: string,
        fromDate?: Date,
        toDate?: Date
    ) {
        return this.httpClient.post(
            environment.ApiURL +
                "api/v1/Reports/renter/transaction/export/" +
                type,
            {
                renterId: renterId,
                fromDate: fromDate,
                toDate: toDate,
                renterName: renterName,
            },
            {
                responseType: "blob",
            }
        );
    }

    ExportOwnerBalance(
        type: string,
        ownerId?: any,
        fromDate?: Date,
        toDate?: Date
    ) {
        return this.httpClient.post(
            environment.ApiURL + "api/v1/Reports/owner/balance/export/" + type,
            { ownerId: ownerId, fromDate: fromDate, toDate: toDate },
            {
                responseType: "blob",
            }
        );
    }

    ExportOwnerTransactions(
        type: string,
        ownerId: any,
        ownerName: string,
        fromDate?: Date,
        toDate?: Date
    ) {
        return this.httpClient.post(
            environment.ApiURL +
                "api/v1/Reports/owner/transaction/export/" +
                type,
            {
                ownerId: ownerId,
                fromDate: fromDate,
                toDate: toDate,
                ownerName: ownerName,
            },
            {
                responseType: "blob",
            }
        );
    }

    ExportBuildingBalance(
        type: string,
        buildingId?: any,
        fromDate?: Date,
        toDate?: Date
    ) {
        return this.httpClient.post(
            environment.ApiURL +
                "api/v1/Reports/building/balance/export/" +
                type,
            { buildingId: buildingId, fromDate: fromDate, toDate: toDate },
            {
                responseType: "blob",
            }
        );
    }
    ExportBuildingTransactions(
        type: string,
        buildingId: any,
        buildingName: string,
        fromDate?: Date,
        toDate?: Date
    ) {
        return this.httpClient.post(
            environment.ApiURL +
                "api/v1/Reports/building/transaction/export/" +
                type,
            {
                buildingId: buildingId,
                fromDate: fromDate,
                toDate: toDate,
                buildingName: buildingName,
            },
            {
                responseType: "blob",
            }
        );
    }

    ExportUnitBalance(
        type: string,
        unitId?: any,
        fromDate?: Date,
        toDate?: Date
    ) {
        return this.httpClient.post(
            environment.ApiURL + "api/v1/Reports/unit/balance/export/" + type,
            { unitId: unitId, fromDate: fromDate, toDate: toDate },
            {
                responseType: "blob",
            }
        );
    }
    ExportUnitTransactions(
        type: string,
        unitId: any,
        unitName: string,
        fromDate?: Date,
        toDate?: Date
    ) {
        return this.httpClient.post(
            environment.ApiURL +
                "api/v1/Reports/unit/transaction/export/" +
                type,
            {
                unitId: unitId,
                fromDate: fromDate,
                toDate: toDate,
                unitName: unitName,
            },
            {
                responseType: "blob",
            }
        );
    }

    ExportCashBankBalance(
        type: string,
        cashBankId?: any,
        fromDate?: Date,
        toDate?: Date
    ) {
        return this.httpClient.post(
            environment.ApiURL +
                "api/v1/Reports/cash-bank/balance/export/" +
                type,
            { cashBankId: cashBankId, fromDate: fromDate, toDate: toDate },
            {
                responseType: "blob",
            }
        );
    }
    ExportCashBankTransactions(
        type: string,
        cashBankId: any,
        cashBankName: string,
        fromDate?: Date,
        toDate?: Date
    ) {
        return this.httpClient.post(
            environment.ApiURL +
                "api/v1/Reports/cash-bank/transaction/export/" +
                type,
            {
                cashBankId: cashBankId,
                fromDate: fromDate,
                toDate: toDate,
                cashBankName: cashBankName,
            },
            {
                responseType: "blob",
            }
        );
    }

    ExportCurrentUnits(type: string) {
        return this.httpClient.get(
            environment.ApiURL + "api/v1/Reports/unit/current/export/" + type,
            {
                responseType: "blob",
            }
        );
    }
    ExportAvailableUnits(type: string) {
        return this.httpClient.get(
            environment.ApiURL + "api/v1/Reports/unit/available/export/" + type,
            {
                responseType: "blob",
            }
        );
    }
}
