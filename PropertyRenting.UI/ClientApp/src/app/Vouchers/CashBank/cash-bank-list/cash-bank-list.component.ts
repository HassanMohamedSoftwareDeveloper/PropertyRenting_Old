import { Component, OnInit } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { Breadcrumb } from "../../../Models/breadcrumb";
import { CashBank } from "../../../Models/cash-bank";
import { AlertifyService } from "../../../Services/alertify.service";
import { BreadcrumbService } from "../../../Services/breadcrumb.service";
import { CashBankService } from "../../../Services/cash-bank.service";
import { DialogService } from "../../../Services/dialog.service";
import { TranslationService } from "../../../Services/translation.service";

@Component({
    selector: "app-cash-bank-list",
    templateUrl: "./cash-bank-list.component.html",
    styleUrls: ["./cash-bank-list.component.css"],
})
export class CashBankListComponent implements OnInit {
    breadcrumbItems: Breadcrumb[] = [];
    cashes: CashBank[] = [];

    pageNumber = 1;
    totalItems = 0;

    constructor(
        private cashBankService: CashBankService,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private breadcrumbService: BreadcrumbService,
        private dialogService: DialogService
    ) {}

    ngOnInit(): void {
        this.loadData();
        this.breadcrumbItems = this.breadcrumbService.CashBankListItems;
    }

    loadData() {
        this.cashBankService
            .GetByPage(this.pageNumber, environment.PageSize)
            .subscribe(
                (result) => {
                    this.cashes = result.data;
                    this.totalItems = result.totalCount;
                },
                (error) => console.log(error)
            );
    }

    DeleteCash(id: any) {
        const deleteMsg = this.translateService.Translate("DeleteMessage");
        this.dialogService.confirm(deleteMsg).subscribe((res) => {
            if (res) {
                this.cashBankService.Delete(id).subscribe(
                    () => {
                        this.loadData();
                        const successMsg = this.translateService.Translate(
                            "DeletedSuccessfully"
                        );
                        this.alertify.success(successMsg);
                    },
                    (error) => console.log(error)
                );
            }
        });
    }

    GetDataByPage(page: number) {
        this.pageNumber = page;
        this.loadData();
    }

    GetType(id: number) {
        return this.cashBankService.GetType(id);
    }
}
