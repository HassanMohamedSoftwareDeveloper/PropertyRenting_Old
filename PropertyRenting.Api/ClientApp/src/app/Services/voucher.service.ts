import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";
import { Enum } from "../Models/enum";
import { Pagination } from "../Models/pagination";
import { Sanad } from "../Models/sanad";
import { Voucher } from "../Models/voucher";

@Injectable({
    providedIn: "root",
})
export class VoucherService {
    SanadTypes: Enum[] = [
        { id: 1, description: "SanadType.General" },
        { id: 2, description: "SanadType.Renter" },
        { id: 3, description: "SanadType.Owner" },
        { id: 4, description: "SanadType.Contributer" },
    ];

    VoucherState: Enum[] = [
        { id: 1, description: "VoucherState.Open" },
        { id: 2, description: "VoucherState.Posted" },
    ];

    constructor(private httpClient: HttpClient) {}

    GetAllReceipts(): Observable<Sanad[]> {
        return this.httpClient.get<Sanad[]>(
            environment.ApiURL + "api/v1/Voucher/receipts/list"
        );
    }
    GetReceiptById(id: any): Observable<Sanad> {
        return this.httpClient.get<Sanad>(
            environment.ApiURL + "api/v1/Voucher/receipts/byId/" + id
        );
    }
    GetReceiptsByPage(
        pageNumber: number,
        pagesize: number
    ): Observable<Pagination<Sanad>> {
        return this.httpClient.get<Pagination<Sanad>>(
            environment.ApiURL +
                "api/v1/Voucher/receipts/list/byPage/" +
                pageNumber +
                "/" +
                pagesize
        );
    }
    CreateReceipt(sanad: Sanad) {
        return this.httpClient.post(
            environment.ApiURL + "api/v1/Voucher/receipts/create",
            sanad
        );
    }
    UpdateReceipt(id: any, sanad: Sanad) {
        return this.httpClient.put(
            environment.ApiURL + "api/v1/Voucher/receipts/update/" + id,
            sanad
        );
    }
    PostReceipt(id: any) {
        return this.httpClient.post(
            environment.ApiURL + "api/v1/Voucher/receipts/post/" + id,
            {}
        );
    }
    DeleteReceipt(id: any) {
        return this.httpClient.delete(
            environment.ApiURL + "api/v1/Voucher/receipts/delete/" + id,
            {}
        );
    }
    PrintReceipt(id: any) {
        return this.httpClient.get(
            environment.ApiURL + "api/v1/Voucher/receipts/print/" + id,
            {
                responseType: "blob",
            }
        );
    }
    GetAllExchanges(): Observable<Sanad[]> {
        return this.httpClient.get<Sanad[]>(
            environment.ApiURL + "api/v1/Voucher/exchanges/list"
        );
    }
    GetExchangeById(id: any): Observable<Sanad> {
        return this.httpClient.get<Sanad>(
            environment.ApiURL + "api/v1/Voucher/exchanges/byId/" + id
        );
    }
    GetExchangesByPage(
        pageNumber: number,
        pagesize: number
    ): Observable<Pagination<Sanad>> {
        return this.httpClient.get<Pagination<Sanad>>(
            environment.ApiURL +
                "api/v1/Voucher/exchanges/list/byPage/" +
                pageNumber +
                "/" +
                pagesize
        );
    }
    CreateExchange(sanad: Sanad) {
        return this.httpClient.post(
            environment.ApiURL + "api/v1/Voucher/exchanges/create",
            sanad
        );
    }
    UpdateExchange(id: any, sanad: Sanad) {
        return this.httpClient.put(
            environment.ApiURL + "api/v1/Voucher/exchanges/update/" + id,
            sanad
        );
    }
    PostExchange(id: any) {
        return this.httpClient.post(
            environment.ApiURL + "api/v1/Voucher/exchanges/post/" + id,
            {}
        );
    }
    DeleteExchange(id: any) {
        return this.httpClient.delete(
            environment.ApiURL + "api/v1/Voucher/exchanges/delete/" + id,
            {}
        );
    }
    PrintExchange(id: any) {
        return this.httpClient.get(
            environment.ApiURL + "api/v1/Voucher/exchanges/print/" + id,
            {
                responseType: "blob",
            }
        );
    }
    GetAllVouchers(): Observable<Voucher[]> {
        return this.httpClient.get<Voucher[]>(
            environment.ApiURL + "api/v1/Voucher/vouchers/list"
        );
    }
    GetVoucherById(id: any): Observable<Voucher> {
        return this.httpClient.get<Voucher>(
            environment.ApiURL + "api/v1/Voucher/vouchers/byId/" + id
        );
    }
    GetVouchersByPage(
        pageNumber: number,
        pagesize: number
    ): Observable<Pagination<Voucher>> {
        return this.httpClient.get<Pagination<Voucher>>(
            environment.ApiURL +
                "api/v1/Voucher/vouchers/list/byPage/" +
                pageNumber +
                "/" +
                pagesize
        );
    }
    CreateVoucher(voucher: Voucher) {
        return this.httpClient.post(
            environment.ApiURL + "api/v1/Voucher/vouchers/create",
            voucher
        );
    }
    UpdateVoucher(id: any, voucher: Voucher) {
        return this.httpClient.put(
            environment.ApiURL + "api/v1/Voucher/vouchers/update/" + id,
            voucher
        );
    }
    PostVoucher(id: any) {
        return this.httpClient.post(
            environment.ApiURL + "api/v1/Voucher/vouchers/post/" + id,
            {}
        );
    }
    DeleteVoucher(id: any) {
        return this.httpClient.delete(
            environment.ApiURL + "api/v1/Voucher/vouchers/delete/" + id,
            {}
        );
    }

    GetVoucherStateById(id: number): string {
        return this.VoucherState?.find((x) => x.id == id)?.description || "";
    }
    GetSanadTypeById(id: number): string {
        return this.SanadTypes?.find((x) => x.id == id)?.description || "";
    }
}
