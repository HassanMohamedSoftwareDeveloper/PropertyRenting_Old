import { Component, OnInit } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { Breadcrumb } from "../../../Models/breadcrumb";
import { Sanad } from "../../../Models/sanad";
import { AlertifyService } from "../../../Services/alertify.service";
import { BreadcrumbService } from "../../../Services/breadcrumb.service";
import { DialogService } from "../../../Services/dialog.service";
import { TranslationService } from "../../../Services/translation.service";
import { VoucherService } from "../../../Services/voucher.service";

@Component({
    selector: "app-exchange-list",
    templateUrl: "./exchange-list.component.html",
    styleUrls: ["./exchange-list.component.css"],
})
export class ExchangeListComponent implements OnInit {
    breadcrumbItems: Breadcrumb[] = [];
    pageNumber = 1;
    totalItems = 0;

    vouchers: Sanad[] = [];
    constructor(
        private breadcrumbService: BreadcrumbService,
        private voucherService: VoucherService,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private dialogService: DialogService
    ) {}

    ngOnInit(): void {
        this.breadcrumbItems = this.breadcrumbService.ExchangeListItems;

        this.loadVouchers();
    }
    GetDataByPage(page: number) {
        this.pageNumber = page;
        this.loadVouchers();
    }

    loadVouchers() {
        this.voucherService
            .GetExchangesByPage(this.pageNumber, environment.PageSize)
            .subscribe(
                (res) => {
                    this.vouchers = res.data;
                    this.totalItems = res.totalCount;
                },
                (error) => console.log(error)
            );
    }

    DeleteVoucher(id: any) {
        const deleteMsg = this.translateService.Translate("DeleteMessage");
        this.dialogService.confirm(deleteMsg).subscribe((res) => {
            if (res) {
                this.voucherService.DeleteExchange(id).subscribe(
                    (_) => {
                        const successMsg = this.translateService.Translate(
                            "DeletedSuccessfully"
                        );
                        this.alertify.success(successMsg);
                        this.loadVouchers();
                    },
                    (error) => {
                        const msg =
                            this.translateService.Translate("ErrorOccurred");
                        this.alertify.error(msg);
                        console.log(error);
                    }
                );
            }
        });
    }

    GetStateById(id: number) {
        return this.voucherService.GetVoucherStateById(id);
    }
}
