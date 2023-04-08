import { Component, OnInit } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { Breadcrumb } from "../../../Models/breadcrumb";
import { Voucher } from "../../../Models/voucher";
import { AlertifyService } from "../../../Services/alertify.service";
import { BreadcrumbService } from "../../../Services/breadcrumb.service";
import { DialogService } from "../../../Services/dialog.service";
import { TranslationService } from "../../../Services/translation.service";
import { VoucherService } from "../../../Services/voucher.service";

@Component({
    selector: "app-voucher-list",
    templateUrl: "./voucher-list.component.html",
    styleUrls: ["./voucher-list.component.css"],
})
export class VoucherListComponent implements OnInit {
    breadcrumbItems: Breadcrumb[] = [];
    pageNumber = 1;
    totalItems = 0;

    vouchers: Voucher[] = [];
    constructor(
        private breadcrumbService: BreadcrumbService,
        private voucherService: VoucherService,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private dialogService: DialogService
    ) {}

    ngOnInit(): void {
        this.breadcrumbItems = this.breadcrumbService.VoucherListItems;

        this.loadVouchers();
    }
    GetDataByPage(page: number) {
        this.pageNumber = page;
        this.loadVouchers();
    }

    loadVouchers() {
        this.voucherService
            .GetVouchersByPage(this.pageNumber, environment.PageSize)
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
                this.voucherService.DeleteVoucher(id).subscribe(
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
