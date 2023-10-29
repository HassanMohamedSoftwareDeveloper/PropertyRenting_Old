import { Component, OnInit } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { Breadcrumb } from "../../../Models/breadcrumb";
import { ContractAddions } from "../../../Models/contract-addions";
import { AlertifyService } from "../../../Services/alertify.service";
import { BreadcrumbService } from "../../../Services/breadcrumb.service";
import { ContractAddionsService } from "../../../Services/contract-addions.service";
import { DialogService } from "../../../Services/dialog.service";
import { TranslationService } from "../../../Services/translation.service";

@Component({
    selector: "app-contract-addions-list",
    templateUrl: "./contract-addions-list.component.html",
    styleUrls: ["./contract-addions-list.component.css"],
})
export class ContractAddionsListComponent implements OnInit {
    mandatoryId = "6d26195c-46e2-4ef2-ba84-f50174c042ea";
    breadcrumbItems: Breadcrumb[] = [];
    contractAdditions: ContractAddions[] = [];

    pageNumber = 1;
    totalItems = 0;

    constructor(
        private contractAdditionService: ContractAddionsService,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private breadcrumbService: BreadcrumbService,
        private dialogService: DialogService
    ) {}

    ngOnInit(): void {
        this.loadData();
        this.breadcrumbItems = this.breadcrumbService.ContractAdditionListItems;
    }

    loadData() {
        this.contractAdditionService
            .GetByPage(this.pageNumber, environment.PageSize)
            .subscribe(
                (result) => {
                    this.contractAdditions = result.data.filter(
                        (x) => x.id != this.mandatoryId
                    );
                    this.totalItems = result.totalCount;
                },
                (error) => console.log(error)
            );
    }

    DeleteAddition(id: any) {
        const deleteMsg = this.translateService.Translate("DeleteMessage");
        this.dialogService.confirm(deleteMsg).subscribe((res) => {
            if (res) {
                this.contractAdditionService.Delete(id).subscribe(
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
}
