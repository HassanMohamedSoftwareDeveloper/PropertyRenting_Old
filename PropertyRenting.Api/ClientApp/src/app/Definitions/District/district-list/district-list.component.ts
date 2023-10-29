import { Component, OnInit, ViewChild } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { ModalComponent } from "../../../CustomTemplates/modal/modal.component";
import { Breadcrumb } from "../../../Models/breadcrumb";
import { District } from "../../../Models/district";
import { AlertifyService } from "../../../Services/alertify.service";
import { BreadcrumbService } from "../../../Services/breadcrumb.service";
import { DialogService } from "../../../Services/dialog.service";
import { DistrictService } from "../../../Services/district.service";
import { TranslationService } from "../../../Services/translation.service";

@Component({
    selector: "app-district-list",
    templateUrl: "./district-list.component.html",
    styleUrls: ["./district-list.component.css"],
})
export class DistrictListComponent implements OnInit {
    breadcrumbItems: Breadcrumb[] = [];
    districts: District[] = [];
    @ViewChild("modal", { static: false }) modal?: ModalComponent;
    district: District = {
        id: null,
        nameAR: "",
        nameEN: "",
        countryId: null,
        country: "",
        city: "",
        cityId: null,
    };

    pageNumber = 1;
    totalItems = 0;

    constructor(
        private districtService: DistrictService,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private breadcumbService: BreadcrumbService,
        private dialogService: DialogService
    ) {}

    ngOnInit(): void {
        this.LoadDistricts();
        this.breadcrumbItems = this.breadcumbService.DistrictListItems;
    }

    LoadDistricts() {
        this.districtService
            .GetByPage(this.pageNumber, environment.PageSize)
            .subscribe(
                (result) => {
                    this.districts = result.data;
                    this.totalItems = result.totalCount;
                },
                (error) => console.log(error)
            );
    }

    DeleteDistrict(id: any) {
        const deleteMsg = this.translateService.Translate("DeleteMessage");
        this.dialogService.confirm(deleteMsg).subscribe((res) => {
            if (res) {
                this.districtService.DeleteDistrict(id).subscribe(
                    () => {
                        this.LoadDistricts();
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

    ShowAddModal() {
        this.district = {
            id: null,
            nameAR: "",
            nameEN: "",
            countryId: null,
            country: "",
            city: "",
            cityId: null,
        };
        this.modal?.showModal();
    }

    ShowEditModal(district: District) {
        this.district = { ...district };
        this.modal?.showModal();
    }

    HideModal(refreshList: boolean) {
        if (refreshList) {
            this.LoadDistricts();
        }
        this.modal?.hideModal();
    }

    GetDataByPage(page: number) {
        this.pageNumber = page;
        this.LoadDistricts();
        console.log(page);
    }
}
