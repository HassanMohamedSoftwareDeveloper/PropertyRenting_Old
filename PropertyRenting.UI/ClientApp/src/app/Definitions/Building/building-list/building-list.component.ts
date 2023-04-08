import { Component, OnInit } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { Breadcrumb } from "../../../Models/breadcrumb";
import { Building } from "../../../Models/building";
import { AlertifyService } from "../../../Services/alertify.service";
import { BreadcrumbService } from "../../../Services/breadcrumb.service";
import { BuildingService } from "../../../Services/building.service";
import { DialogService } from "../../../Services/dialog.service";
import { TranslationService } from "../../../Services/translation.service";

@Component({
    selector: "app-building-list",
    templateUrl: "./building-list.component.html",
    styleUrls: ["./building-list.component.css"],
})
export class BuildingListComponent implements OnInit {
    breadcrumbItems: Breadcrumb[] = [];
    buildings: Building[] = [];

    pageNumber = 1;
    totalItems = 0;

    constructor(
        private buildingService: BuildingService,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private breadcrumbService: BreadcrumbService,
        private dialogService: DialogService
    ) {}

    ngOnInit(): void {
        this.LoadBuildings();
        this.breadcrumbItems = this.breadcrumbService.BuildingListItems;
    }

    LoadBuildings() {
        this.buildingService
            .GetByPage(this.pageNumber, environment.PageSize)
            .subscribe(
                (result) => {
                    this.buildings = result.data;
                    this.totalItems = result.totalCount;
                },
                (error) => console.log(error)
            );
    }

    DeleteBuilding(id: any) {
        const deleteMsg = this.translateService.Translate("DeleteMessage");
        this.dialogService.confirm(deleteMsg).subscribe((res) => {
            if (res) {
                this.buildingService.DeleteBuilding(id).subscribe(
                    () => {
                        this.LoadBuildings();
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
        this.LoadBuildings();
        console.log(page);
    }
}
