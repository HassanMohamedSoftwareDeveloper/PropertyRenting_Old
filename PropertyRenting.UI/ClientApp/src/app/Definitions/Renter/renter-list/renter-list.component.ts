import { Component, OnInit } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { Breadcrumb } from "../../../Models/breadcrumb";
import { Renter } from "../../../Models/renter";
import { AlertifyService } from "../../../Services/alertify.service";
import { BreadcrumbService } from "../../../Services/breadcrumb.service";
import { DialogService } from "../../../Services/dialog.service";
import { RenterService } from "../../../Services/renter.service";
import { TranslationService } from "../../../Services/translation.service";

@Component({
    selector: "app-renter-list",
    templateUrl: "./renter-list.component.html",
    styleUrls: ["./renter-list.component.css"],
})
export class RenterListComponent implements OnInit {
    breadcrumbItems: Breadcrumb[] = [];
    renters: Renter[] = [];

    pageNumber = 1;
    totalItems = 0;

    constructor(
        private renterService: RenterService,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private breadcrumbService: BreadcrumbService,
        private dialogService: DialogService
    ) {}

    ngOnInit(): void {
        this.loadRenters();
        this.breadcrumbItems = this.breadcrumbService.RenterListItems;
    }

    loadRenters() {
        this.renterService
            .GetByPage(this.pageNumber, environment.PageSize)
            .subscribe(
                (result) => {
                    this.renters = result.data;
                    this.totalItems = result.totalCount;
                },
                (error) => console.log(error)
            );
    }

    DeleteRenter(id: any) {
        const deleteMsg = this.translateService.Translate("DeleteMessage");
        this.dialogService.confirm(deleteMsg).subscribe((res) => {
            if (res) {
                this.renterService.Delete(id).subscribe(
                    () => {
                        this.loadRenters();
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
        this.loadRenters();
        console.log(page);
    }
}
