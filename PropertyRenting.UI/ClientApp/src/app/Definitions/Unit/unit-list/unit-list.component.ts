import { Component, OnInit } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { Breadcrumb } from "../../../Models/breadcrumb";
import { Unit } from "../../../Models/unit";
import { AlertifyService } from "../../../Services/alertify.service";
import { BreadcrumbService } from "../../../Services/breadcrumb.service";
import { DialogService } from "../../../Services/dialog.service";
import { TranslationService } from "../../../Services/translation.service";
import { UnitService } from "../../../Services/unit.service";

@Component({
    selector: "app-unit-list",
    templateUrl: "./unit-list.component.html",
    styleUrls: ["./unit-list.component.css"],
})
export class UnitListComponent implements OnInit {
    breadcrumbItems: Breadcrumb[] = [];
    units: Unit[] = [];

    pageNumber = 1;
    totalItems = 0;

    constructor(
        private unitService: UnitService,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private breadcrumbService: BreadcrumbService,
        private dialogService: DialogService
    ) {}

    ngOnInit(): void {
        this.loadUnits();
        this.breadcrumbItems = this.breadcrumbService.UnitListItems;
    }

    loadUnits() {
        this.unitService
            .GetByPage(this.pageNumber, environment.PageSize)
            .subscribe(
                (result) => {
                    this.units = result.data;
                    this.totalItems = result.totalCount;
                },
                (error) => console.log(error)
            );
    }

    DeleteUnit(id: any) {
        const deleteMsg = this.translateService.Translate("DeleteMessage");
        this.dialogService.confirm(deleteMsg).subscribe((res) => {
            if (res) {
                this.unitService.Delete(id).subscribe(
                    () => {
                        this.loadUnits();
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
        this.loadUnits();
        console.log(page);
    }
}
